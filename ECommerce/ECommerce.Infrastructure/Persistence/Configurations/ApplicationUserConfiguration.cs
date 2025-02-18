using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.DEFAULT_STRING_LENGTH);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(Constants.DEFAULT_STRING_LENGTH);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();
    }
}
