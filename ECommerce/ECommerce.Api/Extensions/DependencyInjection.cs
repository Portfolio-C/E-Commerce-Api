using ECommerce.Application.Extensions;
using ECommerce.Infrastructure.Extensions;

namespace ECommerce.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterApplication();
        services.RegisterInfrastructure(configuration);

        return services;
    }
}
