namespace Core.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; }
    public DateTime ExpiresUtc { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public bool IsActive { get; set; }
}