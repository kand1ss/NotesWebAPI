using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Email).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Login).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).HasMaxLength(24);
        builder.Property(x => x.LastName).HasMaxLength(32);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(128);
        
        builder.HasIndex(x => x.Login).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}