using Application.DTO;
using Core.Models;

namespace Application.Contracts;

public interface IAccountValidator
{
    Task Validate(RegisterAccountDTO registerData);
    Task ValidateLogin(string login);
    Task ValidateEmail(string email);
}