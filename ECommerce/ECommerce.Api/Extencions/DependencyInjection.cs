using ECommerce.Infrastructure.Extensions;

namespace ECommerce.Api.Extencions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructure(configuration);

        return services;
    }
}
