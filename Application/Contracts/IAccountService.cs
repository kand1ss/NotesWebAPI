using Application.DTO;

namespace Application.Contracts;

public interface IAccountService
{
    Task RegisterAsync(RegisterAccountDTO registerData);
    Task<TokensDTO> LoginAsync(LoginAccountDTO loginData, string ipAddress, string userAgent);
    Task<TokensDTO> RefreshLoginAsync(string refreshToken, string ipAddress, string userAgent);
    Task UpdateAsync(Guid id, UpdateAccountDTO account);
    Task DeleteAsync(Guid id);

    Task<AccountDTO> GetAccountByLoginAsync(string login);
    Task<AccountDTO> GetAccountByEmailAsync(string email);
    Task<AccountDTO> GetAccountByIdAsync(Guid id);
    Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();
}