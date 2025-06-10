using Microsoft.AspNetCore.Http;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Presentation.Results;

/// <summary>
///     Provides methods for creating API results based on domain results.
/// </summary>
public static class ApiResults
{
    /// <summary>
    ///     Translates a <see cref="Result" /> into an <see cref="IResult" /> that represents a problem response.
    /// </summary>
    /// <param name="result"><see cref="Result" /> instance.</param>
    /// <returns><see cref="IResult" /> instance which represents a problem response.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the given <paramref name="result" /> is a success.</exception>
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        return Microsoft.AspNetCore.Http.Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.ErrorType),
            statusCode: GetStatusCode(result.Error.ErrorType),
            extensions: GetErrors(result));

        static string GetTitle(Error error)
        {
            return error.ErrorType switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Server failure"
            };
        }

        static string GetDetail(Error error)
        {
            return error.ErrorType switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.Problem => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                _ => "An unexpected error occurred"
            };
        }

        static string GetType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        }

        static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError) return null;

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}