using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Presentation.Results;

/// <summary>
///     Provides extension methods for working with <see cref="Result" /> and <see cref="Result{T}" /> types.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Matches a <see cref="Result" /> to either a success or a failure case and executes the corresponding function.
    /// </summary>
    /// <typeparam name="TOut">The return type of the match operation.</typeparam>
    /// <param name="result">The <see cref="Result" /> instance to match.</param>
    /// <param name="onSuccess">The function to execute if the result is a success.</param>
    /// <param name="onFailure">
    ///     The function to execute if the result is a failure. The failed result is passed as an argument
    ///     to this function.
    /// </param>
    /// <returns>
    ///     The value returned by the executed function, either <paramref name="onSuccess" /> or <paramref name="onFailure" />.
    /// </returns>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    ///     Matches a <see cref="Result{TIn}" /> to either a success or a failure case and executes the corresponding function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the result.</typeparam>
    /// <typeparam name="TOut">The return type of the match operation.</typeparam>
    /// <param name="result">The <see cref="Result{TIn}" /> instance to match.</param>
    /// <param name="onSuccess">
    ///     The function to execute if the result is a success. The value of the result is passed as an
    ///     argument to this function.
    /// </param>
    /// <param name="onFailure">
    ///     The function to execute if the result is a failure. The failed result is passed as an argument
    ///     to this function.
    /// </param>
    /// <returns>
    ///     The value returned by the executed function, either <paramref name="onSuccess" /> or <paramref name="onFailure" />.
    /// </returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}