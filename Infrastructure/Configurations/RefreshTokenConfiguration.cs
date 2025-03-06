using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Token).IsRequired().HasMaxLength(256);
        builder.Property(x => x.ExpiresUtc).IsRequired();
        builder.Property(x => x.IpAddress).IsRequired();
        builder.Property(x => x.UserAgent).IsRequired().HasMaxLength(500);
        
        builder.HasOne(x => x.Account)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AccountId);
    }
}