using Microsoft.EntityFrameworkCore;
using Testerke.Modules.Users.Application.Abstractions;
using Testerke.Modules.Users.Domain.Users;
using Testerke.Modules.Users.Infrastructure.Users;

namespace Testerke.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}