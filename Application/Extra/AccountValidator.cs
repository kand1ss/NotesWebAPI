using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.DTO;
using Core.Contracts;

namespace Application.Extra;

public class AccountValidator(IAccountRepository repository) : IAccountValidator
{
    public async Task ValidateAsync(RegisterAccountDTO registerData)
    {
        var accountByLogin = await repository.GetByLoginAsync(registerData.Login);
        if(accountByLogin is not null)
            throw new ValidationException($"Account with login \"{registerData.Login}\" already exists.");
        
        var accountByEmail = await repository.GetByEmailAsync(registerData.Email);
        if(accountByEmail is not null)
            throw new ValidationException($"Account with email \"{registerData.Email}\" already exists.");
    }

    public async Task ValidateLoginAsync(string login)
    {
        var account = await repository.GetByLoginAsync(login);
        if(account is null)
            throw new ValidationException($"Account with login \"{login}\" does not exist.");
    }

    public async Task ValidateEmailAsync(string email)
    {
        var account = await repository.GetByEmailAsync(email);
        if(account is null)
            throw new ValidationException($"Account with email \"{email}\" does not exist.");
    }
}