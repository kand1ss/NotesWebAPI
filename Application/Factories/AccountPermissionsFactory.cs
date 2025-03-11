using Core.Models;

namespace Application.Factories;

public static class AccountPermissionsFactory
{
    public static AccountPermission Create(Account account, Permission permission)
        => new()
        {
            Account = account,
            Permission = permission,
        };
    
    public static IEnumerable<AccountPermission> Create(Account account, IEnumerable<Permission> permissions)
        => permissions.Select(p => Create(account, p));
}