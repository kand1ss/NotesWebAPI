using Application.DTO;
using Core.Models;

namespace Application.Contracts;

public interface IAccountUpdater
{
    void Update(Account account, UpdateAccountDTO updateData);
}