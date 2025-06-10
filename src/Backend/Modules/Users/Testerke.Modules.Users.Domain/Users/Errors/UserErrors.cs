using Testerke.Common.Domain.Monads;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Domain.Users.Errors;

/// <summary>
///     Defines error codes and messages related to user operations.
/// </summary>
public static class UserErrors
{
    /// <summary>
    ///     Error indicating that a user with the specified ID was not found.
    /// </summary>
    /// <param name="userId">Strongly typed identifier of the searched user.</param>
    /// <returns><see cref="Error" /> instance.</returns>
    public static Error NotFound(UserId userId)
    {
        return Error.NotFound(
            "Users.NotFound",
            $"User with ID '{userId.Id}' was not found.");
    }
}