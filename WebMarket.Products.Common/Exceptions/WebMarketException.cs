namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Базовое исключение для ошибок, вызванных клиентом.
/// </summary>
public abstract class WebMarketException(ErrorKind kind, string errorCode, string message, Exception? innerException = null) : Exception(message, innerException)
{
    public ErrorKind Kind { get; } = kind;

    /// <summary>
    /// Код ошибки.
    /// </summary>
    public string ErrorCode { get; } = errorCode;
}
