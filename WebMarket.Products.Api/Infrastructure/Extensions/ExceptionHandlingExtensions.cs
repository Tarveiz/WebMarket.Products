using Microsoft.AspNetCore.Mvc;
using WebMarket.Products.Api.Infrastructure.ExceptionHandling;

namespace WebMarket.Products.Api.Infrastructure.Extensions;

internal static class ExceptionHandlingExtensions
{
    /// <summary>
    /// Регистрирует глобальный обработчик исключений и единый формат ошибок ProblemDetails (RFC 9457).
    /// </summary>
    public static IServiceCollection AddWebMarketExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options => options.CustomizeProblemDetails = ProblemDetailsEnricher.Enrich);
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.Configure<JsonOptions>(options => options.AllowInputFormatterExceptionMessages = false);
        return services;
    }

    /// <summary>
    /// Подключает обработку исключений и ProblemDetails для пустых ответов 4xx/5xx.
    /// </summary>
    public static IApplicationBuilder UseWebMarketExceptionHandling(this IApplicationBuilder app) => app.UseExceptionHandler().UseStatusCodePages();
}
