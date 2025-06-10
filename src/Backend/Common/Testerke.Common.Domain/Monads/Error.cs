namespace Testerke.Common.Domain.Monads;

/// <summary>
///     Provides a record type for representing errors in a domain-driven design context.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Description">A description of the error.</param>
/// <param name="ErrorType">Type of the error.</param>
public record Error(string Code, string Description, ErrorType ErrorType)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static readonly Error NullValue = new(
        "General.NullValue",
        "A null value was provided",
        ErrorType.Failure);

    /// <summary>
    ///     Creates a new error with the specified code and description, categorized as a failure.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">A description of the error.</param>
    /// <returns>
    ///     <see cref="Error" /> instance with specified <paramref name="code" /> and <paramref name="description" />,
    ///     categorized as a <see cref="Monads.ErrorType.Failure" />.
    /// </returns>
    public static Error Failure(string code, string description)
    {
        return new Error(code, description, ErrorType.Failure);
    }

    /// <summary>
    ///     Creates a new error with the specified code and description, categorized as a problem.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">A description of the error.</param>
    /// <returns>
    ///     <see cref="Error" /> instance with specified <paramref name="code" /> and <paramref name="description" />,
    ///     categorized as a <see cref="Monads.ErrorType.Problem" />.
    /// </returns>
    public static Error Problem(string code, string description)
    {
        return new Error(code, description, ErrorType.Problem);
    }

    /// <summary>
    ///     Creates a new error with the specified code and description, categorized as not found.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">A description of the error.</param>
    /// <returns>
    ///     <see cref="Error" /> instance with specified <paramref name="code" /> and <paramref name="description" />,
    ///     categorized as <see cref="Monads.ErrorType.NotFound" />.
    /// </returns>
    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    /// <summary>
    ///     Creates a new error with the specified code and description, categorized as a conflict.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">A description of the error.</param>
    /// <returns>
    ///     <see cref="Error" /> instance with specified <paramref name="code" /> and <paramref name="description" />,
    ///     categorized as a <see cref="Monads.ErrorType.Conflict" />.
    /// </returns>
    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }
}