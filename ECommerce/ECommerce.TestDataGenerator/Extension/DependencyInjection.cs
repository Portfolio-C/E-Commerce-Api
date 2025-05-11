using System;
using ECommerce.TestDataGenerator.Configurations;
using ECommerce.TestDataGenerator.Factories;
using ECommerce.TestDataGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.TestDataGenerator.Extension;

public static class DependencyInjection
{
    public static IServiceCollection AddTestDataGenerator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDatabaseSeederFactory, DatabaseSeederFactory>();

        services.AddConfigurationSettings(configuration);

        return services;
    }

    private static void AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DataSeedSettings>()
            .Bind(configuration.GetSection(nameof(DataSeedSettings)))
            .ValidateOnStart();
    }
}
