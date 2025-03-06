using Application.DTO;
using Core.Models;

namespace Application.Mappers;

public static class AccountMapper
{
    public static AccountDTO ToDTO(this Account account)
        => new(account.Login, account.Email, account.Name, account.LastName);
    
    public static IEnumerable<AccountDTO> ToDTOs(this IEnumerable<Account> accounts)
        => accounts.Select(ToDTO);
}