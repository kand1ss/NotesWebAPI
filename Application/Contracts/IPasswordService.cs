using Core.Models;

namespace Application.Contracts;

public interface IPasswordService
{
    string Generate(Account account, string password);
    bool Verify(Account account, string password);
}