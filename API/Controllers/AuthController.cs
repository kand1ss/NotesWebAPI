using Application.Contracts;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/accounts")]
public class AuthController(IAccountService accountService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccountDTO registerData)
    {
        await accountService.Register(registerData);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginAccountDTO loginData)
    {
        await accountService.Login(loginData);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountDTO updateData)
    {
        await accountService.Update(id, updateData);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        await accountService.Delete(id);
        return Ok();
    }
    

    [HttpGet("{login}")]
    public async Task<IActionResult> GetAccountByLogin(string login)
    {
        var account = await accountService.GetAccountByLogin(login);
        return Ok(account);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccountById(Guid id)
    {
        var account = await accountService.GetAccountById(id);
        return Ok(account);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await accountService.GetAllAccounts();
        return Ok(accounts);
    }
}