using Application.DTO;

namespace Application.Contracts;

public interface IAccountService
{
    Task Register(RegisterAccountDTO registerData);
    Task Login(LoginAccountDTO loginData);
    Task Update(Guid id, UpdateAccountDTO account);
    Task Delete(Guid id);

    Task<AccountDTO> GetAccountByLogin(string login);
    Task<AccountDTO> GetAccountByEmail(string email);
    Task<AccountDTO> GetAccountById(Guid id);
    Task<IEnumerable<AccountDTO>> GetAllAccounts();
}