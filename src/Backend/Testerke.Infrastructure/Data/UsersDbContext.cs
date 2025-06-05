using Microsoft.EntityFrameworkCore;
using Testerke.Application.Abstractions;
using Testerke.Domain.Users;
using Testerke.Infrastructure.Users;

namespace Testerke.Infrastructure.Data;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}