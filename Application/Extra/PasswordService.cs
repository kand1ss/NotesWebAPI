using Application.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Extra;

public class PasswordService(IPasswordHasher<Account> hasher) : IPasswordService
{
    public string Generate(Account account, string password)
        => hasher.HashPassword(account, password);
    
    public bool Verify(Account account, string password)
        => hasher
            .VerifyHashedPassword(account, account.PasswordHash, password) == PasswordVerificationResult.Success;
}