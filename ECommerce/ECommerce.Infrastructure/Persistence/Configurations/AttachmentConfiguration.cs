using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

internal sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(Constants.DEFAULT_STRING_LENGTH);

        builder.Property(a => a.FileType)
            .IsRequired();

        builder.Property(a => a.FileData)
            .IsRequired();

        builder.HasOne(a => a.Product)
            .WithMany(p => p.Attachments)
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
