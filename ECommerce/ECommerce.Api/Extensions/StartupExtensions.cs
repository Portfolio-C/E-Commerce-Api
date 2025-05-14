using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.TestDataGenerator.Configurations;
using ECommerce.TestDataGenerator.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerce.Api.Extensions;

public static class StartupExtensions
{
    public static async Task<IApplicationBuilder> UseDatabaseSeederAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seederFactory = scope.ServiceProvider.GetRequiredService<IDatabaseSeederFactory>();
        var seeder = seederFactory.CreateSeeder(app.Environment.EnvironmentName);
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var settings = scope.ServiceProvider.GetRequiredService<IOptions<DataSeedSettings>>();

        await seeder.SeedDatabaseAsync(context, userManager, settings.Value);

        return app;
    }
}
