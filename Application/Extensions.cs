using Application.Contracts;
using Application.Extra;
using Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddExtraServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAccountUpdater, AccountUpdater>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        
        return services;
    }
}