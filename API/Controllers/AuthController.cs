using System.Security.Claims;
using API.Extra;
using Application.Contracts;
using Application.DTO;
using Application.Extra;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[ApiController]
[Route("/api/accounts")]
public class AuthController(IAccountService accountService, CookiesManager cookies,
    IOptions<AuthSettings> authConfiguration) : ControllerBase
{
    private UserData GetUserData()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        if (string.IsNullOrEmpty(userAgent))
            userAgent = "unknown";
        if (string.IsNullOrEmpty(ipAddress))
            ipAddress = "unknown";
        
        return new UserData(ipAddress, userAgent);
    }

    private string? GetUserIdFromClaim()
        => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    
    
    [HttpPost]
    public async Task<IActionResult> RegisterAccountAsync([FromBody] RegisterAccountDTO registerData)
    {
        await accountService.RegisterAsync(registerData);
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginAccountDTO loginData)
    {
        var userData = GetUserData();
        var tokens = await accountService.LoginAsync(loginData, userData.IpAddress, userData.UserAgent);
        
        cookies.AppendTokens(tokens, authConfiguration.Value);
        return Ok();
    }

    [HttpPost("login/refresh")]
    public async Task<IActionResult> RefreshLoginAsync()
    {
        var userData = GetUserData();
        var token = cookies.Get(TokenNames.RefreshToken);
        if(string.IsNullOrEmpty(token))
            return Unauthorized();
        
        var newTokens = await accountService.RefreshLoginAsync(token, userData.IpAddress, userData.UserAgent);
        cookies.AppendTokens(newTokens, authConfiguration.Value);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAccountAsync([FromBody] UpdateAccountDTO updateData)
    {
        var id = GetUserIdFromClaim();
        if(string.IsNullOrEmpty(id))
            return Unauthorized();
        
        await accountService.UpdateAsync(new Guid(id), updateData);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccountAsync()
    {
        var id = GetUserIdFromClaim();
        if(string.IsNullOrEmpty(id))
            return Unauthorized();
        
        await accountService.DeleteAsync(new Guid(id));
        return Ok();
    }
    

    [HttpGet("{login}")]
    public async Task<IActionResult> GetAccountByLoginAsync(string login)
    {
        var account = await accountService.GetAccountByLoginAsync(login);
        return Ok(account);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetAccountByIdAsync()
    {
        var id = GetUserIdFromClaim();
        var account = await accountService.GetAccountByIdAsync(new Guid(id));
        
        return Ok(account);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var accounts = await accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }
}