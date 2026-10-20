using WebMarket.Products.Common.Exceptions;
using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Api.Infrastructure.ExceptionHandling;

/// <summary>
/// Сопоставление категорий ошибок и HTTP-статусов с кодами ошибок.
/// </summary>
internal static class ProblemDetailsMapper
{
    public static int ToStatusCode(this ErrorKind kind) => kind switch
    {
        ErrorKind.Validation => StatusCodes.Status400BadRequest,
        ErrorKind.Unauthenticated => StatusCodes.Status401Unauthorized,
        ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
        ErrorKind.NotFound => StatusCodes.Status404NotFound,
        ErrorKind.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };

    /// <summary>
    /// Код ошибки для ответов, сформированных не из WebMarketException.
    /// </summary>
    public static string GetDefaultErrorCode(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => ErrorCodes.Common.ValidationFailed,
        StatusCodes.Status401Unauthorized => ErrorCodes.Common.Unauthenticated,
        StatusCodes.Status403Forbidden => ErrorCodes.Common.Forbidden,
        StatusCodes.Status404NotFound => ErrorCodes.Common.NotFound,
        StatusCodes.Status405MethodNotAllowed => ErrorCodes.Common.MethodNotAllowed,
        StatusCodes.Status409Conflict => ErrorCodes.Common.Conflict,
        StatusCodes.Status413PayloadTooLarge => ErrorCodes.Common.PayloadTooLarge,
        StatusCodes.Status415UnsupportedMediaType => ErrorCodes.Common.UnsupportedMediaType,
        StatusCodes.Status429TooManyRequests => ErrorCodes.Common.TooManyRequests,
        >= StatusCodes.Status500InternalServerError => ErrorCodes.Common.InternalError,
        _ => ErrorCodes.Common.RequestFailed
    };
}
