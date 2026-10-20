using WebMarket.Products.Api.Infrastructure.Extensions;

namespace WebMarket.Products.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddWebMarketExceptionHandling();
    }

    private static void Configure(WebApplication app)
    {
        app.UseWebMarketExceptionHandling(); // Глобальная обработка ошибок

        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
