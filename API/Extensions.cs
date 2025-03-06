using API.Exception_Filters;

namespace API;

public static class Extensions
{
    public static IServiceCollection AddFilteredControllers(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add<AuthExceptionFilter>();
        });
        return services;
    }
}