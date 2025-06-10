using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace Testerke.Common.Domain.Guards;

/// <summary>
///     Class which contains guard clauses for validating the length of a string.
/// </summary>
public static class LengthGuard
{
    /// <summary>
    ///     Throws an <see cref="ArgumentException" /> if the length of the <paramref name="input" /> string is greater than
    ///     <paramref name="maxLength" />.
    /// </summary>
    /// <param name="_"><see cref="IGuardClause" /> instance (unused).</param>
    /// <param name="input">The string to validate the length of.</param>
    /// <param name="maxLength">The maximum length of the input string.</param>
    /// <param name="paramName">The name of the parameter used in the exception message.</param>
    /// <returns>The <paramref name="input" /> string if the check succeeds.</returns>
    /// <exception cref="ArgumentException">
    ///     If the lenght of <paramref name="input" /> is greater than
    ///     <paramref name="maxLength" />.
    /// </exception>
    public static string Length(
        this IGuardClause _,
        string input,
        int maxLength,
        [CallerArgumentExpression(nameof(input))]
        string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        if (input.Length > maxLength)
            throw new ArgumentException($"Should not exceed maximum length of {maxLength} characters.", paramName);
        return input;
    }
}