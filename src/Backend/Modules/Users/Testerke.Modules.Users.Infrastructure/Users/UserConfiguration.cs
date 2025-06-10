using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testerke.Modules.Users.Domain.Users;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Id,
                value => UserId.Create(value));

        builder.Property(u => u.Email).HasMaxLength(100);

        builder.Property(u => u.FirstName).HasMaxLength(100);

        builder.Property(u => u.LastName).HasMaxLength(200);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}