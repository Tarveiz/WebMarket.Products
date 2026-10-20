using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Конфликт с текущим состоянием ресурса (409).
/// </summary>
public sealed class ConflictException(string message, string errorCode = ErrorCodes.Common.Conflict, Exception? innerException = null) : WebMarketException(ErrorKind.Conflict, errorCode, message, innerException);
