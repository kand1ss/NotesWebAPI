using Application.DTO;
using Core.Models;

namespace Application.Contracts;

public interface IAccountValidator
{
    Task ValidateAsync(RegisterAccountDTO registerData);
    Task ValidateLoginAsync(string login);
    Task ValidateEmailAsync(string email);
}