using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Products.Common.Exceptions;
using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Api.Infrastructure.ExceptionHandling;

/// <summary>
/// Глобальный обработчик исключений под формат RFC 9457 и логирует его с нужным уровнем.
/// </summary>
internal sealed partial class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string UnexpectedErrorMessage = "An unexpected error occurred.";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetails(exception);
        var statusCode = problemDetails.Status!.Value;
        var request = httpContext.Request;

        if (statusCode >= StatusCodes.Status500InternalServerError)
            LogServerError(exception, request.Method, request.Path);
        else
            LogClientError(request.Method, request.Path, statusCode, problemDetails.Extensions[ProblemDetailsEnricher.ErrorCodeKey], problemDetails.Detail);

        httpContext.Response.StatusCode = statusCode;
        var problemContext = new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails, Exception = exception };
        if (!await problemDetailsService.TryWriteAsync(problemContext)) // Клиент не принимает JSON
        {
            ProblemDetailsEnricher.Enrich(problemContext);
            await TypedResults.Problem(problemDetails).ExecuteAsync(httpContext);
        }

        return true;
    }

    private static ProblemDetails CreateProblemDetails(Exception exception)
    {
        switch (exception)
        {
            case WebMarketException webMarketException:
                var problemDetails = webMarketException is ValidationException { Errors: not null } validationException ? new HttpValidationProblemDetails(validationException.Errors) : new ProblemDetails();
                problemDetails.Status = webMarketException.Kind.ToStatusCode();
                problemDetails.Detail = webMarketException.Message;
                problemDetails.Extensions[ProblemDetailsEnricher.ErrorCodeKey] = webMarketException.ErrorCode;
                return problemDetails;

            case BadHttpRequestException badRequestException:
                return new ProblemDetails { Status = badRequestException.StatusCode, Detail = badRequestException.Message, Extensions = { [ProblemDetailsEnricher.ErrorCodeKey] = ProblemDetailsMapper.GetDefaultErrorCode(badRequestException.StatusCode) } };

            default:
                return new ProblemDetails { Status = StatusCodes.Status500InternalServerError, Detail = UnexpectedErrorMessage, Extensions = { [ProblemDetailsEnricher.ErrorCodeKey] = ErrorCodes.Common.InternalError } };
        }
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Request {Method} {Path} rejected with {StatusCode} ({ErrorCode}): {Detail}")]
    private partial void LogClientError(string method, string path, int statusCode, object? errorCode, string? detail);

    [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception while processing {Method} {Path}")]
    private partial void LogServerError(Exception exception, string method, string path);
}
