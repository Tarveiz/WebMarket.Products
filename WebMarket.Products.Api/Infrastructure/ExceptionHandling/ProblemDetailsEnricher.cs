using System.Diagnostics;

namespace WebMarket.Products.Api.Infrastructure.ExceptionHandling;

/// <summary>
/// Дополняет любой ProblemDetails сервиса (RFC 9457) единым набором полей: instance, errorCode, requestId, traceId.
/// Применяется ко всем источникам: GlobalExceptionHandler, валидация модели, status code pages, ProblemDetailsFactory.
/// </summary>
internal static class ProblemDetailsEnricher
{
    public const string ErrorCodeKey = "errorCode";
    private const string RequestIdKey = "requestId";
    private const string TraceIdKey = "traceId";

    public static void Enrich(ProblemDetailsContext context)
    {
        var problemDetails = context.ProblemDetails;
        var httpContext = context.HttpContext;

        problemDetails.Instance ??= httpContext.Request.Path.Value;
        problemDetails.Extensions.TryAdd(ErrorCodeKey, ProblemDetailsMapper.GetDefaultErrorCode(problemDetails.Status ?? httpContext.Response.StatusCode));
        problemDetails.Extensions[RequestIdKey] = httpContext.TraceIdentifier;
        problemDetails.Extensions[TraceIdKey] = Activity.Current?.TraceId.ToHexString() ?? httpContext.TraceIdentifier;
    }
}
