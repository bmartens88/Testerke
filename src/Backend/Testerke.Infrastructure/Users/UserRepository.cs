using Microsoft.EntityFrameworkCore;
using Testerke.Domain.Users;
using Testerke.Domain.Users.Abstractions;
using Testerke.Domain.Users.ValueObjects;
using Testerke.Infrastructure.Data;

namespace Testerke.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}