using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Services;

namespace Portfolio.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPortfolioPublicService, PortfolioPublicService>();
        services.AddScoped<IPortfolioAdminService, PortfolioAdminService>();
        return services;
    }
}
