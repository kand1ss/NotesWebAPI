using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountPermissionConfiguration : IEntityTypeConfiguration<AccountPermission>
{
    public void Configure(EntityTypeBuilder<AccountPermission> builder)
    {
        builder.HasKey(p => new { p.AccountId, p.PermissionId });
        
        builder.HasOne(p => p.Account)
            .WithMany(a => a.Permissions)
            .HasForeignKey(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Permission)
            .WithMany(x => x.Accounts)
            .HasForeignKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}