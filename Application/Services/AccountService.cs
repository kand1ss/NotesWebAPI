using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.DTO;
using Application.Exceptions;
using Application.Extra;
using Application.Factories;
using Application.Mappers;
using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class AccountService(IAccountValidator accountValidator, IAccountRepository accountRepository,
    IPasswordService passwordService, IAccountUpdater accountUpdater, JWTService jwtService,
    ITokenRepository tokenRepository, IAccountPermissionsRepository permissionsRepository) : IAccountService
{
    private async Task<Account> TryGetAccountByIdAsync(Guid id)
    {
        var account = await accountRepository.GetByIdAsync(id);
        if (account is null)
            throw new AccountNotFoundException(id);
        
        return account;
    }
    private async Task<Account> TryGetAccountByLoginAsync(string login)
    {
        var account = await accountRepository.GetByLoginAsync(login);
        if (account is null)
            throw new AccountNotFoundException(login);

        return account;
    }


    public async Task RegisterAsync(RegisterAccountDTO registerData)
    {
        await accountValidator.ValidateAsync(registerData);

        var account = AccountFactory.Create(registerData);
        account.PasswordHash = passwordService.Generate(account, registerData.Password);
        
        var defaultPermissions = await permissionsRepository.GetDefaultPermissions();
        var accountPermissions = AccountPermissionsFactory.Create(account, defaultPermissions);

        await accountRepository.CreateAsync(account);
        await permissionsRepository.AddPermissions(accountPermissions);
    }

    public async Task<TokensDTO> LoginAsync(LoginAccountDTO loginData, string ipAddress, string userAgent)
    {
        var login = loginData.Login;
        await accountValidator.ValidateLoginAsync(login);
        
        var account = await TryGetAccountByLoginAsync(login);
        if (!passwordService.Verify(account, loginData.Password))
            throw new ValidationException($"Password doesn't match account \"{login}\"");
        
        return await UpdateTokensAsync(account, ipAddress, userAgent);
    }

    private async Task<TokensDTO> UpdateTokensAsync(Account account, string ipAddress, string userAgent)
    {
        var tokens = jwtService.GenerateTokens(account, ipAddress, userAgent);
        var refreshTokensById = await tokenRepository.GetByAccountIdAsync(account.Id);
        var equalRefreshTokens = refreshTokensById
            .Where(t => t.IpAddress == ipAddress && t.UserAgent == userAgent).ToList();

        if (equalRefreshTokens.Any())
            await tokenRepository.RemoveRangeAsync(equalRefreshTokens);

        await tokenRepository.CreateAsync(tokens.RefreshToken);
        return tokens;
    }

    public async Task<TokensDTO> RefreshLoginAsync(string refreshToken, string ipAddress, string userAgent)
    {
        var tokenInfo = await tokenRepository.GetByTokenAsync(refreshToken);
        RefreshTokenValidator.Validate(tokenInfo, ipAddress, userAgent);
        
        var account = await TryGetAccountByIdAsync(tokenInfo.AccountId);
        return await UpdateTokensAsync(account, ipAddress, userAgent);
    }

    public async Task UpdateAsync(Guid id, UpdateAccountDTO updateData)
    {
        var account = await TryGetAccountByIdAsync(id);
        accountUpdater.Update(account, updateData);
        
        await accountRepository.UpdateAsync(account);
    }

    public async Task DeleteAsync(Guid id)
    {
        var account = await TryGetAccountByIdAsync(id);
        await accountRepository.DeleteAsync(account);
    }

    public async Task<AccountDTO> GetAccountByLoginAsync(string login)
    {
        var account = await TryGetAccountByLoginAsync(login);
        return account.ToDTO();
    }

    public async Task<AccountDTO> GetAccountByEmailAsync(string email)
    {
        var account = await accountRepository.GetByEmailAsync(email);
        if (account is null)
            throw new AccountNotFoundException(email);
        
        return account.ToDTO();
    }

    public async Task<AccountDTO> GetAccountByIdAsync(Guid id)
    {
        var account = await TryGetAccountByIdAsync(id);
        return account.ToDTO();
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
    {
        var accounts = await accountRepository.GetAllAsync();
        return accounts.ToDTOs();
    }
}