using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTO;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extra;

public class JWTService(IOptions<AuthSettings> authConfiguration)
{
    public TokensDTO GenerateTokens(Account account, string ipAddress, string userAgent)
    {
        var accessToken = GenerateAccessToken(account);
        var refreshToken = GenerateRefreshToken(account, ipAddress, userAgent);
        
        return new TokensDTO(accessToken, refreshToken);
    }

    private string GenerateAccessToken(Account account)
    {
        var accessTokenExpires = DateTime.UtcNow.Add(authConfiguration.Value.AccessTokenLifetime);
        var secretKey = Encoding.UTF8.GetBytes(authConfiguration.Value.SecretKey);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Name, account.Login)
        };

        var accessToken = new JwtSecurityToken(
            expires: accessTokenExpires,
            claims: claims,
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }

    private RefreshToken GenerateRefreshToken(Account account, string ipAddress, string userAgent)
    {
        var refreshTokenExpires = DateTime.UtcNow.Add(authConfiguration.Value.RefreshTokenLifetime);
        
        var numbers = new byte[32];
        RandomNumberGenerator.Fill(numbers);
        
        var token = Convert.ToBase64String(numbers);
        return new RefreshToken
        {
            Token = token,
            AccountId = account.Id,
            CreatedUtc = DateTime.UtcNow,
            ExpiresUtc = refreshTokenExpires,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            IsActive = true
        };
    }
}