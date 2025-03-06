using Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class RefreshTokenCleanupService(IServiceScopeFactory scopeFactory, ILogger<RefreshTokenCleanupService> logger) 
    : IHostedService, IDisposable
{
    private readonly TimeSpan _cleaningPeriod = TimeSpan.FromHours(24);
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CleanupExpiredTokens, null, TimeSpan.Zero, _cleaningPeriod);
        logger.LogInformation("Service started");
        return Task.CompletedTask;
    }

    private async void CleanupExpiredTokens(object? state)
    {
        using var scope = scopeFactory.CreateScope();
        
        var tokenRepository = scope.ServiceProvider.GetRequiredService<ITokenRepository>();
        var expiredTokens = (await tokenRepository.GetExpiredTokensAsync()).ToList();
        logger.LogInformation($"A {expiredTokens.Count} number of expired tokens have been found");
        
        await tokenRepository.RemoveRangeAsync(expiredTokens);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}