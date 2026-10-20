namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Категория ошибки. Сервисный слой оперирует категорией, а HTTP-статус назначает GlobalExceptionHandler.
/// </summary>
public enum ErrorKind
{
    Validation,
    Unauthenticated,
    Forbidden,
    NotFound,
    Conflict
}
