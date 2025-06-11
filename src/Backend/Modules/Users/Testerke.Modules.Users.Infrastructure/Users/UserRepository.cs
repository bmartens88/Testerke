using Microsoft.EntityFrameworkCore;
using Testerke.Modules.Users.Domain.Users;
using Testerke.Modules.Users.Domain.Users.Abstractions;
using Testerke.Modules.Users.Domain.Users.ValueObjects;
using Testerke.Modules.Users.Infrastructure.Database;

namespace Testerke.Modules.Users.Infrastructure.Users;

/// <summary>
///     Implementation of the <see cref="IUserRepository" /> interface.
/// </summary>
/// <param name="dbContext"><see cref="UsersDbContext" /> instance for persistence data store access.</param>
internal sealed class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    private readonly UsersDbContext _dbContext = dbContext;

    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }
}