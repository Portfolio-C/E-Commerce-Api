using ECommerce.Domain.Interfaces;
using ECommerce.TestDataGenerator.Configurations;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.TestDataGenerator.Interfaces;

public interface IDatabaseSeeder
{
    Task SeedDatabaseAsync(
        IApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        DataSeedSettings settings);
}
