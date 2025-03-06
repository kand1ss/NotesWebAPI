using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository(DataContext context) : ITokenRepository
{
    public async Task CreateAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RefreshToken>> GetByAccountIdAsync(Guid id)
        => await context.RefreshTokens.Where(x => x.AccountId == id).ToListAsync();
    public async Task<IEnumerable<RefreshToken>> GetExpiredTokensAsync()
        => await context.RefreshTokens.Where(x => x.ExpiresUtc < DateTime.UtcNow).ToListAsync();
    
    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);

    public async Task RemoveRangeAsync(IEnumerable<RefreshToken> refreshTokens)
    {
        context.RefreshTokens.RemoveRange(refreshTokens);
        await context.SaveChangesAsync();
    }
}