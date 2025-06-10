namespace Testerke.Modules.Users.Application.Abstractions;

/// <summary>
///     Defines a contract for a unit of work that encapsulates a set of operations that can be committed or rolled back.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Saves all changes made in this unit of work to the underlying data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>Number of changes persisted to underlying data store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}