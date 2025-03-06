using API.Exception_Filters;
using API.Extra;

namespace API;

public static class Extensions
{
    public static IServiceCollection AddAPIManagers(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<CookiesManager>();

        return services;
    }
    
    public static IServiceCollection AddFilteredControllers(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add<AuthExceptionFilter>();
        });
        return services;
    }
}