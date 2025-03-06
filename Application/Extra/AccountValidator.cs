using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.DTO;
using Core.Contracts;

namespace Application.Extra;

public class AccountValidator(IAccountRepository repository) : IAccountValidator
{
    public async Task Validate(RegisterAccountDTO registerData)
    {
        var accountByLogin = await repository.GetByLogin(registerData.Login);
        if(accountByLogin is not null)
            throw new ValidationException($"Account with login \"{registerData.Login}\" already exists.");
        
        var accountByEmail = await repository.GetByEmail(registerData.Email);
        if(accountByEmail is not null)
            throw new ValidationException($"Account with email \"{registerData.Email}\" already exists.");
    }

    public async Task ValidateLogin(string login)
    {
        var account = await repository.GetByLogin(login);
        if(account is null)
            throw new ValidationException($"Account with login \"{login}\" does not exist.");
    }

    public async Task ValidateEmail(string email)
    {
        var account = await repository.GetByEmail(email);
        if(account is null)
            throw new ValidationException($"Account with email \"{email}\" does not exist.");
    }
}