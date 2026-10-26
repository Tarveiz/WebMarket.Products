using WebMarket.Products.Api.Infrastructure.Extensions;
using WebMarket.Products.Common.Data;
using WebMarket.Products.Common.Helpers.Extensions;
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
        services.AddWebMarketExceptionHandling();
        services.AddWebMarketSwagger();

        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().SetPreflightMaxAge(TimeSpan.FromHours(1))));

        services.AddWebMarketData(configuration.GetRequiredSection(nameof(MasterDbSecret)).Get<MasterDbSecret>()!);
        services.AddCqrsHandlers(typeof(WebMarketDbContext).Assembly);
    }

    private static void Configure(WebApplication app)
    {
        app.UseWebMarketExceptionHandling(); // Глобальная обработка ошибок
        app.UseWebMarketSwagger();

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
    }
}
