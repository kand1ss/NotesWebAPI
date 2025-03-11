using Core.Models;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<AccountPermission> AccountPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new AccountPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}