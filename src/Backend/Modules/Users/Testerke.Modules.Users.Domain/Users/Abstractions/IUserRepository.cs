using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Domain.Users.Abstractions;

/// <summary>
///     Defines the contract for a user repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The strongly typed identifier of the user to search.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="User" /> when found; otherwise <c>null</c>.</returns>
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a new user to the repository.
    /// </summary>
    /// <param name="user"><see cref="User" /> instance to add.</param>
    void Insert(User user);
}