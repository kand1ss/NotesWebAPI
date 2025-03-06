using Application.Contracts;
using Application.DTO;
using Core.Models;

namespace Application.Extra;

public class AccountUpdater(IPasswordService passwordService) : IAccountUpdater
{
    public void Update(Account account, UpdateAccountDTO updateData)
    {
        account.Login = updateData.Login ?? account.Login;
        account.Email = updateData.Email ?? account.Email;
        account.Name = updateData.Name ?? account.Name;
        account.LastName = updateData.LastName ?? account.LastName;
        
        if(!string.IsNullOrEmpty(updateData.Password))
            account.PasswordHash = passwordService.Generate(account, updateData.Password);
    }
}