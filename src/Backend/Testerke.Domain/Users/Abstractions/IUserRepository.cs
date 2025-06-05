using Testerke.Domain.Users.ValueObjects;

namespace Testerke.Domain.Users.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    void Insert(User user);
}