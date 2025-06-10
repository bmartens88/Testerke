namespace Testerke.Common.Domain.Monads;

/// <summary>
///     Provides a record type for representing errors in a domain-driven design context.
/// </summary>
/// <param name="Errors">Nested errors.</param>
public record ValidationError(Error[] Errors) : Error(
    "General.ValidationError",
    "One or more validation errors occurred.",
    ErrorType.Validation)
{
    /// <summary>
    ///     Create a <see cref="ValidationError" /> from a collection of <see cref="Result" /> objects.
    /// </summary>
    /// <param name="results">Results of which the error will be used.</param>
    /// <returns>A new <see cref="ValidationError" /> instance.</returns>
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results.Where(result => result.IsFailure).Select(result => result.Error).ToArray());
    }
}