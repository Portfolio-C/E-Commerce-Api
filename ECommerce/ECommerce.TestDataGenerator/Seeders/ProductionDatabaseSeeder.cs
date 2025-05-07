using System;
using ECommerce.Domain.Interfaces;
using ECommerce.TestDataGenerator.Configurations;
using ECommerce.TestDataGenerator.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.TestDataGenerator.Seeders;

public sealed class ProductionDatabaseSeeder() : IDatabaseSeeder
{
    public Task SeedDatabaseAsync(IApplicationDbContext context, UserManager<IdentityUser> userManager, DataSeedSettings settings)
    {
        throw new NotImplementedException();
    }
}
