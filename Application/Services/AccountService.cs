using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.DTO;
using Application.Exceptions;
using Application.Factories;
using Application.Mappers;
using Core.Contracts;
using Core.Models;

namespace Application.Services;

public class AccountService(IAccountValidator accountValidator, IAccountRepository accountRepository,
    IPasswordService passwordService, IAccountUpdater accountUpdater) : IAccountService
{
    private async Task<Account> TryGetAccountById(Guid id)
    {
        var account = await accountRepository.GetById(id);
        if (account is null)
            throw new AccountNotFoundException(id);
        
        return account;
    }
    private async Task<Account> TryGetAccountByLogin(string login)
    {
        var account = await accountRepository.GetByLogin(login);
        if (account is null)
            throw new AccountNotFoundException(login);

        return account;
    }


    public async Task Register(RegisterAccountDTO registerData)
    {
        await accountValidator.Validate(registerData);

        var account = AccountFactory.Create(registerData);
        account.PasswordHash = passwordService.Generate(account, registerData.Password);

        await accountRepository.Create(account);
    }

    public async Task Login(LoginAccountDTO loginData)
    {
        var login = loginData.Login;
        await accountValidator.ValidateLogin(login);
        
        var account = await TryGetAccountByLogin(login);
        if (!passwordService.Verify(account, loginData.Password))
            throw new ValidationException($"Password doesn't match account \"{login}\"");

        // TODO - JWT аутентификация
    }

    public async Task Update(Guid id, UpdateAccountDTO updateData)
    {
        var account = await TryGetAccountById(id);
        accountUpdater.Update(account, updateData);
        
        await accountRepository.Update(account);
    }

    public async Task Delete(Guid id)
    {
        var account = await TryGetAccountById(id);
        await accountRepository.Delete(account);
    }

    public async Task<AccountDTO> GetAccountByLogin(string login)
    {
        var account = await TryGetAccountByLogin(login);
        return account.ToDTO();
    }

    public async Task<AccountDTO> GetAccountByEmail(string email)
    {
        var account = await accountRepository.GetByEmail(email);
        if (account is null)
            throw new AccountNotFoundException(email);
        
        return account.ToDTO();
    }

    public async Task<AccountDTO> GetAccountById(Guid id)
    {
        var account = await TryGetAccountById(id);
        return account.ToDTO();
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccounts()
    {
        var accounts = await accountRepository.GetAll();
        return accounts.ToDTOs();
    }
}