using Application.DTO;
using Application.Extra;

namespace API.Extra;

public class CookiesManager(IHttpContextAccessor contextAccessor)
{
    public void Append(string key, string value, CookieOptions? options = null)
    {
        var context = contextAccessor.HttpContext;
        if (context is null)
            return;
        
        context.Response.Cookies.Append(key, value, options ?? new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

    public void AppendTokens(TokensDTO tokens, AuthSettings authSettings)
    {
        var context = contextAccessor.HttpContext;
        if (context is null)
            return;
        
        context.Response.Cookies.Append(TokenNames.AccessToken, tokens.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.Add(authSettings.AccessTokenLifetime)
        });
        
        context.Response.Cookies.Append(TokenNames.RefreshToken, tokens.RefreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.Add(authSettings.RefreshTokenLifetime)
        });
    }

    public string? Get(string key)
        => contextAccessor.HttpContext?.Request.Cookies[key];

    public void Delete(string key)
        => contextAccessor.HttpContext?.Response.Cookies.Delete(key);
}