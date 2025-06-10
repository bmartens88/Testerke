using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Testerke.Common.Application.Messaging;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Behaviors;

/// <summary>
///     <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation that validates requests using a collection of
///     validators.
/// </summary>
/// <param name="validators"><see cref="IEnumerable{T}" /> of <see cref="IValidator{T}" /> used to validate requests.</param>
/// <param name="logger"><see cref="ILogger{TCategoryName}" /> implementation for logging purposes.</param>
/// <typeparam name="TRequest">Type of the request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var failures = await ValidateAsync(_validators.ToList(), request, cancellationToken);

        if (failures.Length is 0)
            return await next(cancellationToken);

        if (typeof(TResponse) == typeof(Result))
            return Unsafe.As<TResponse>(Result.Failure(CreateValidationError(failures)));
        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));
            if (failureMethod is not null)
                return Unsafe.As<TResponse>(failureMethod.Invoke(null, [CreateValidationError(failures)]))!;
        }

        throw new ValidationException(failures);
    }

    /// <summary>
    ///     Performs asynchronous validation of the request using the provided validators.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request.</typeparam>
    /// <param name="validatorsCore">The validators for the current request to use.</param>
    /// <param name="request">The request to validate.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> wich represents the asynchronous operation. Returns an array of
    ///     <see cref="ValidationFailure" />.
    /// </returns>
    private static async Task<ValidationFailure[]> ValidateAsync(
        List<IValidator<TRequest>> validatorsCore,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        if (validatorsCore.Count is 0)
            return [];

        var validationContext = new ValidationContext<TRequest>(request);

        var validationTasks =
            validatorsCore.Select(validator => validator.ValidateAsync(validationContext, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToArray();

        return failures;
    }

    /// <summary>
    ///     Helper method to create a <see cref="ValidationError" /> from an array of <see cref="ValidationFailure" />.
    /// </summary>
    /// <param name="failures">Array of <see cref="ValidationFailure" />.</param>
    /// <returns><see cref="ValidationError" /> instance to return.</returns>
    private static ValidationError CreateValidationError(ValidationFailure[] failures)
    {
        return new ValidationError(failures
            .Select(error => Error.Problem(error.ErrorCode, error.ErrorMessage))
            .ToArray());
    }
}