using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationsUsers { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Attachment> Attachments { get; set; }

    DbSet<RefreshToken> RefreshTokens { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
