using Testerke.Modules.Users.Domain.Users;
using Testerke.Modules.Users.Domain.Users.Abstractions;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Insert(User user)
    {
        throw new NotImplementedException();
    }
}