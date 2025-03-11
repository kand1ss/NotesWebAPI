namespace Core.Models;

public class Account
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime ModifiedUtc { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<AccountPermission> Permissions { get; set; }
    public ICollection<Note> Notes { get; set; }
}