using System.Diagnostics.CodeAnalysis;

namespace Testerke.Common.Domain.Monads;

/// <summary>
///     Represents the result of an operation, encapsulating success or failure.
/// </summary>
public class Result
{
    /// <summary>
    ///     Constructs a new instance of the <see cref="Result" /> class.
    /// </summary>
    /// <param name="isSuccess">Whether the result is a success or not.</param>
    /// <param name="error">The error to encapsulate, if any.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when there is a mismatch between <paramref name="isSuccess" /> and
    ///     <paramref name="error" />.
    /// </exception>
    protected Result(bool isSuccess, Error error)
    {
        if ((!isSuccess && error == Error.None) ||
            (isSuccess && error != Error.None))
            throw new ArgumentException("Invalid error", nameof(error));
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    /// <summary>
    ///     Constructs a successful result with no error.
    /// </summary>
    /// <returns><see cref="Result" /> resembling a success.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    ///     Construct a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error to encapsulate within the result.</param>
    /// <returns><see cref="Result" /> resembling a failure.</returns>
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    /// <summary>
    ///     Constructs a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to encapsulate within the result.</param>
    /// <typeparam name="TValue">The type of <paramref name="value" />.</typeparam>
    /// <returns><see cref="Result{TValue}" /> resembling a success with the given <paramref name="value" />.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    ///     Constructs a failed result with the specified error for a specific value type.
    /// </summary>
    /// <param name="error">The error to encapsulate within the result.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns><see cref="Result{TValue}" /> resembling a failure with the given <paramref name="error" />.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}

/// <summary>
///     Represents a result that encapsulates a value, success status, and an error.
/// </summary>
/// <param name="value">A value to encapsulate within the result.</param>
/// <param name="isSuccess">Whether the result is a success or failure.</param>
/// <param name="error">The error to encapsulate within the result.</param>
/// <typeparam name="TValue">Type of <paramref name="value" />.</typeparam>
public class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    private readonly TValue? _value = value;

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failed result cannot be accessed.");

    /// <summary>
    ///     Implicitly converts a value of type <typeparamref name="TValue" /> to a <see cref="Result{TValue}" />.
    /// </summary>
    /// <typeparam name="TValue">Type of <paramref name="value" />.</typeparam>
    /// <param name="value">The value to encapsulate within the result.</param>
    /// <returns></returns>
    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    /// <summary>
    ///     Constructs a <see cref="Result{TValue}" /> as a failure.
    /// </summary>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="error">The error to encapsulate within the result.</param>
    /// <returns><see cref="Result{TValue}" /> resembling a failure with the given <paramref name="error" />.</returns>
    /// <remarks>
    ///     This method is used by validation middleware to construct a response when validation fails. In that case,
    ///     <paramref name="error" /> will be of type <see cref="ValidationError" />.
    /// </remarks>
    public static Result<TValue> ValidationFailure(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}