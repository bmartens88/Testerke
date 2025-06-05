using MediatR;
using Microsoft.Extensions.Logging;
using Testerke.Application.Exceptions;

namespace Testerke.Application.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception for '{RequestName}'", typeof(TRequest).Name);

            throw new TesterkeException(typeof(TRequest).Name, innerException: ex);
        }
    }
}