using System.Text;
using Application.Contracts;
using Application.Extra;
using Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddExtraServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAccountUpdater, AccountUpdater>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        services.AddHostedService<RefreshTokenCleanupService>();
        
        return services;
    }

    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfiguration = configuration.GetSection("AuthSettings");
        var authSettings = authConfiguration.Get<AuthSettings>();
        services.Configure<AuthSettings>(authConfiguration);
        services.AddScoped<JWTService>();

        ConfigureAuthentication(services, authSettings);

        return services;
    }

    private static void ConfigureAuthentication(IServiceCollection services, AuthSettings authSettings)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(authSettings.SecretKey))
            };

            opt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey(TokenNames.AccessToken))
                        context.Token = context.Request.Cookies[TokenNames.AccessToken];

                    return Task.CompletedTask;
                }
            };
        });
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<INoteService, NoteService>();
        
        return services;
    }
}