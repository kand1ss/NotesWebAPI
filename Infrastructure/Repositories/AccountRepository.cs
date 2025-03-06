using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(DataContext context) : IAccountRepository
{
    public async Task CreateAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        context.Accounts.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account account)
    {
        context.Accounts.Remove(account);
        await context.SaveChangesAsync();
    }

    public async Task<Account?> GetByLoginAsync(string login)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Login == login);

    public async Task<Account?> GetByEmailAsync(string email)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<Account?> GetByIdAsync(Guid id)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Account>> GetAllAsync()
        => await context.Accounts.ToListAsync();
}