namespace Core.Models;

public class Permission
{
    public int Id { get; set; }
    public string PermissionName { get; set; }
    public ICollection<AccountPermission> Accounts { get; set; }
}