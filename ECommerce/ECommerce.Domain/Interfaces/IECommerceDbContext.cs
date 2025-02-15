using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Domain.Interfaces;
public interface IECommerceDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Attachment> Attachments { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
