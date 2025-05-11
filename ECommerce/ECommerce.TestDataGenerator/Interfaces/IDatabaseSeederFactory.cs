using System;

namespace ECommerce.TestDataGenerator.Interfaces;

public interface IDatabaseSeederFactory
{
    IDatabaseSeeder CreateSeeder(string environment);
}
