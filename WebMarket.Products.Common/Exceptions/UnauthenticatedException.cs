using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Exceptions;

/// <summary>
/// Пользователь не аутентифицирован (401).
/// </summary>
public sealed class UnauthenticatedException(string message = "Authentication is required.", string errorCode = ErrorCodes.Common.Unauthenticated) : WebMarketException(ErrorKind.Unauthenticated, errorCode, message);
