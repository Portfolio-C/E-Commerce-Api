using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.TestDataGenerator.Configurations;

public sealed class DataSeedSettings
{
    public const string SectionName = nameof(DataSeedSettings);

    [Range(0, double.MaxValue, ErrorMessage = "Invalid number of categories.")]
    public int NumberOfCategories { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Invalid number of products.")]
    public int NumberOfProducts { get; set; }

}
