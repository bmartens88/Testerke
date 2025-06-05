namespace Testerke.SharedKernel.Monads;

public record ValidationError(Error[] Errors) : Error(
    "General.ValidationError",
    "One or more validation errors occurred",
    ErrorType.Validation)
{
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToArray());
    }
}