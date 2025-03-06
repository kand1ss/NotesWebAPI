using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Application.Extra;

public static class RefreshTokenValidator
{
    public static void Validate(RefreshToken tokenInfo, string ipAddress, string userAgent)
    {
        if (tokenInfo is null)
            throw new ValidationException("The refresh token is invalid or does not exist.");

        var refreshToken = tokenInfo.Token;
        
        if (!tokenInfo.IsActive)
            throw new ValidationException($"The refresh token \"{refreshToken}\" is not active.");

        if (tokenInfo.ExpiresUtc < DateTime.UtcNow)
            throw new ValidationException($"The refresh token \"{refreshToken}\" has expired.");

        if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(userAgent))
            throw new ValidationException("IP address or UserAgent cannot be null or empty.");

        if (tokenInfo.IpAddress != ipAddress || tokenInfo.UserAgent != userAgent)
            throw new ValidationException("Either the IP address or UserAgent does not match the refresh token information.");
    }
}