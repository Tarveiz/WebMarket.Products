using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Npgsql;
using WebMarket.Products.Api.Infrastructure.Extensions;
using WebMarket.Products.Api.Infrastructure.Swagger;
using WebMarket.Products.Common.Data;
using WebMarket.Products.Common.Helpers.Extensions;
using WebMarket.Products.Common.Helpers.Mappers;
using WebMarket.Products.Common.Settings.Secrets;

namespace WebMarket.Products.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddVaultSecrets();
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddWebMarketExceptionHandling();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebMarket Products API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT access token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
            options.SupportNonNullableReferenceTypes();
            options.EnableAnnotations();
            options.OrderActionsBy(SwaggerActionOrderHelper.GetSortKey);
        });

        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetPreflightMaxAge(TimeSpan.FromHours(1));
            });
        });

        var masterDbSecret = configuration.GetSection(nameof(MasterDbSecret)).Get<MasterDbSecret>() ?? throw new InvalidOperationException($"Secret '{nameof(MasterDbSecret)}' not found");
        services.AddSingleton(_ => NpgsqlDataSource.Create(masterDbSecret.ToConnectionString()));

        services.AddDbContextPool<WebMarketDbContext>((provider, options) =>
        {
            var dataSource = provider.GetRequiredService<NpgsqlDataSource>();
            options.UseNpgsql(dataSource).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddCqrsHandlers(typeof(WebMarketDbContext).Assembly);
    }

    private static void Configure(WebApplication app)
    {
        app.UseWebMarketExceptionHandling(); // Глобальная обработка ошибок

        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebMarket Products API v1");
                options.DisplayRequestDuration();
                options.EnablePersistAuthorization();
            });
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
    }
}
