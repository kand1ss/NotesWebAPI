using Application.DTO;
using Core.Models;

namespace Application.Factories;

public static class AccountFactory
{
    public static Account Create(RegisterAccountDTO account)
        => new()
        {
            Id = Guid.NewGuid(),
            Login = account.Login,
            Email = account.Email,
            Name = account.Name,
            LastName = account.LastName,
            CreatedUtc = DateTime.UtcNow
        };
}