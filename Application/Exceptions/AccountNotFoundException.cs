
namespace Application.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string key) : base($"Account \"{key}\" was not found.")
    {
    }
    public AccountNotFoundException(Guid id) : base($"Account with id \"{id}\" was not found.")
    {
    }
}