using System;
using ECommerce.TestDataGenerator.Configurations;
using ECommerce.TestDataGenerator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using ECommerce.TestDataGenerator.Seeders;

namespace ECommerce.TestDataGenerator.Factories;

public class DatabaseSeederFactory(IServiceScopeFactory serviceScopeFactory) : IDatabaseSeederFactory
{
    public IDatabaseSeeder CreateSeeder(string environment)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var settings = scope.ServiceProvider.GetRequiredService<IOptions<DataSeedSettings>>().Value;

        return environment.ToLowerInvariant() switch
        {
            "development" => new DevelopmentDatabaseSeeder(),
            "production" => new ProductionDatabaseSeeder(),
            _ => throw new ArgumentOutOfRangeException($"Could not resolve environment: {environment}.")
        };
    }
}
