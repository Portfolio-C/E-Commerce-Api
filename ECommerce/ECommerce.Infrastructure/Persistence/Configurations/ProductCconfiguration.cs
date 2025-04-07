using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.DEFAULT_STRING_LENGTH);

        builder.Property(p => p.Description)
            .HasMaxLength(Constants.MAX_STRING_LENGTH);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasMany(p => p.Favorites)
            .WithOne(f => f.Product)
            .HasForeignKey(f => f.ProductId);

        builder.HasMany(p => p.Baskets)
               .WithOne(b => b.Product)
               .HasForeignKey(b => b.ProductId);

        builder.HasMany(p => p.OrderItems)
               .WithOne(oi => oi.Product)
               .HasForeignKey(oi => oi.ProductId);
    }
}
