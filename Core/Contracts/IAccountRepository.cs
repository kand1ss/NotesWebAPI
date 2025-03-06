using Core.Models;

namespace Core.Contracts;

public interface IAccountRepository
{
    public Task CreateAsync(Account account);
    public Task UpdateAsync(Account account);
    public Task DeleteAsync(Account account);
    
    public Task<Account?> GetByLoginAsync(string login);
    public Task<Account?> GetByEmailAsync(string email);
    public Task<Account?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Account>> GetAllAsync();
}