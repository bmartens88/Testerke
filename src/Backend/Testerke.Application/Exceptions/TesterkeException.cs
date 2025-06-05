using Testerke.SharedKernel.Monads;

namespace Testerke.Application.Exceptions;

public sealed class TesterkeException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}