using Core.Models;

namespace Core.Contracts;

public interface IAccountRepository
{
    public Task Create(Account account);
    public Task Update(Account account);
    public Task Delete(Account account);
    
    public Task<Account?> GetByLogin(string login);
    public Task<Account?> GetByEmail(string email);
    public Task<Account?> GetById(Guid id);
    public Task<IEnumerable<Account>> GetAll();
    
    public Task<bool> IsLoginTaken(string login);
    public Task<bool> IsEmailTaken(string email);
}