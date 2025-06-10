using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Exceptions;

/// <summary>
///     Represents an application-level exception in the Testerke application.
/// </summary>
/// <param name="requestName">Name of the request in which the exception was thrown.</param>
/// <param name="error">Error object which might have caused the exception to be thrown.</param>
/// <param name="innerException">Inner exception.</param>
public sealed class TesterkeException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}