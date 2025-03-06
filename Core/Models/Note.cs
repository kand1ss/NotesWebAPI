namespace Core.Models;

public class Note
{
    public int Id { get; set; }
    public Guid AccountId { get; set; }
    public Account User { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime ModifiedUtc { get; set; }
}