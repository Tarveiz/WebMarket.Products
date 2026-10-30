using WebMarket.Products.Common.Exceptions;

namespace WebMarket.Products.Common.Helpers.Validators;

/// <summary>
/// Проверка параметров запроса
/// </summary>
public static class RequestValidationHelper
{
    /// <summary>
    /// Парсит обязательный идентификатор
    /// </summary>
    public static Guid ParseRequiredId(string? value, string message, string errorCode) => Guid.TryParse(value, out var id) && id != Guid.Empty ? id : throw new ValidationException(message, errorCode);
}
