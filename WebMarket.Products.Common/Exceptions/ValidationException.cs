using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Некорректные входные данные (400).
/// </summary>
public sealed class ValidationException(string message, string errorCode = ErrorCodes.Common.ValidationFailed, IReadOnlyDictionary<string, string[]>? errors = null) : WebMarketException(ErrorKind.Validation, errorCode, message)
{
    public IReadOnlyDictionary<string, string[]>? Errors { get; } = errors;
}
