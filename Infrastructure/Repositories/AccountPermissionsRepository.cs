using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountPermissionsRepository(DataContext context) : IAccountPermissionsRepository
{
    public async Task<IEnumerable<Permission>> GetAccountPermissions(Guid accountId)
        => await context.AccountPermissions
            .Where(p => p.AccountId == accountId)
            .Select(p => p.Permission).ToListAsync();
    
    public async Task<IEnumerable<Permission>> GetDefaultPermissions()
        => await context.Permissions.Where(x => x.Id == 1 || x.Id == 2).ToListAsync();

    public async Task AddPermission(AccountPermission permission)
    {
        await context.AccountPermissions.AddAsync(permission);
        await context.SaveChangesAsync();
    }

    public async Task AddPermissions(IEnumerable<AccountPermission> permissions)
    {
        foreach(var permission in permissions)
            await AddPermission(permission);
    }

    public async Task RemovePermission(AccountPermission permission)
    {
        context.AccountPermissions.Remove(permission);
        await context.SaveChangesAsync();
    }
}