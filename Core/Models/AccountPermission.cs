namespace Core.Models;

public class AccountPermission
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}