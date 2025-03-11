using Core.Models;

namespace Core.Contracts;

public interface IAccountPermissionsRepository
{
    Task<IEnumerable<Permission>> GetAccountPermissions(Guid accountId);
    Task<IEnumerable<Permission>> GetDefaultPermissions();
    Task AddPermission(AccountPermission permission);
    Task AddPermissions(IEnumerable<AccountPermission> permissions);
    Task RemovePermission(AccountPermission permission);
}