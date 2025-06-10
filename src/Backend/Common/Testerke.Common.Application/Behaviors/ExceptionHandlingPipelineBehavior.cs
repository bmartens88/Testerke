using MediatR;
using Microsoft.Extensions.Logging;
using Testerke.Common.Application.Exceptions;

namespace Testerke.Common.Application.Behaviors;

/// <summary>
///     <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation that handles exceptions thrown during the
///     processing of requests.
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}" /> implementation for logging purposes.</param>
/// <typeparam name="TRequest">Type of the request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger = logger;

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred while processing request {RequestType}",
                typeof(TRequest).Name);
            throw new TesterkeException(typeof(TRequest).Name, innerException: ex);
        }
    }
}