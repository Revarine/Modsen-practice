using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Models;

namespace Shop.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasMany(e => e.Orders)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
    }
}