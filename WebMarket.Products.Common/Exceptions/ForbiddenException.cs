using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Недостаточно прав для операции (403).
/// </summary>
public sealed class ForbiddenException(string message = "Access to the resource is denied.", string errorCode = ErrorCodes.Common.Forbidden) : WebMarketException(ErrorKind.Forbidden, errorCode, message);
