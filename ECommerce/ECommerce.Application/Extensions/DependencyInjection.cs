using ECommerce.Application.Interfaces;
using ECommerce.Application.Mappings;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryMappings).Assembly);

        AddServices(services);
        
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<ICateogryService, CategoryService>();
    }
}
