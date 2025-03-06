using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(DataContext context) : IAccountRepository
{
    public async Task Create(Account account)
    {
        await context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();
    }

    public async Task Update(Account account)
    {
        context.Accounts.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Account account)
    {
        context.Accounts.Remove(account);
        await context.SaveChangesAsync();
    }

    public async Task<Account?> GetByLogin(string login)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Login == login);

    public async Task<Account?> GetByEmail(string email)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<Account?> GetById(Guid id)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Account>> GetAll()
        => await context.Accounts.ToListAsync();

    
    public async Task<bool> IsLoginTaken(string login)
        => await context.Accounts.AnyAsync(x => x.Login == login);

    public async Task<bool> IsEmailTaken(string email)
        => await context.Accounts.AnyAsync(x => x.Email == email);
}