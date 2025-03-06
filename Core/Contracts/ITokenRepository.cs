using Core.Models;

namespace Core.Contracts;

public interface ITokenRepository
{
    Task CreateAsync(RefreshToken refreshToken);
    Task<IEnumerable<RefreshToken>> GetByAccountIdAsync(Guid id);
    Task<IEnumerable<RefreshToken>> GetExpiredTokensAsync();
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RemoveRangeAsync(IEnumerable<RefreshToken> refreshTokens);
}