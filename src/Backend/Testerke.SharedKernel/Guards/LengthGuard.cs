using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace Testerke.SharedKernel.Guards;

public static class LengthGuard
{
    public static string Length(
        this IGuardClause _,
        string input,
        int maxLength,
        [CallerArgumentExpression(nameof(input))]
        string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);

        if (input.Length <= maxLength)
            return input;
        throw new ArgumentException($"Should not exceed maximum length of {maxLength}", paramName);
    }
}