using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Ресурс не найден (404).
/// </summary>
public sealed class NotFoundException(string message, string errorCode = ErrorCodes.Common.NotFound) : WebMarketException(ErrorKind.NotFound, errorCode, message);
