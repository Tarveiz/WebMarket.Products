namespace WebMarket.Products.Common.Settings.Constants;

/// <summary>
/// Коды ошибок: не переименовываются и не переиспользуются, только добавляются.
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// Общие коды.
    /// TODO: перенести в nugget-библиотеки.
    /// </summary>
    public static class Common
    {
        public const string ValidationFailed = "validation_failed";
        public const string Unauthenticated = "unauthenticated";
        public const string Forbidden = "forbidden";
        public const string NotFound = "not_found";
        public const string MethodNotAllowed = "method_not_allowed";
        public const string Conflict = "conflict";
        public const string PayloadTooLarge = "payload_too_large";
        public const string UnsupportedMediaType = "unsupported_media_type";
        public const string TooManyRequests = "too_many_requests";
        public const string RequestFailed = "request_failed";
        public const string InternalError = "internal_error";
    }
}
