using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance;
public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : IdentityDbContext(options), IECommerceDbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region entities

        builder.Entity<IdentityUser>(e =>
        {
            e.ToTable("Account");
        });

        builder.Entity<IdentityRole>(e =>
        {
            e.ToTable("Role");
        });

        builder.Entity<IdentityUserClaim<string>>(e =>
        {
            e.ToTable("UserClaim");
        });

        builder.Entity<IdentityUserLogin<string>>(e =>
        {
            e.ToTable("UserLogin");
        });

        builder.Entity<IdentityUserToken<string>>(e =>
        {
            e.ToTable("UserToken");
        });

        builder.Entity<IdentityRoleClaim<string>>(e =>
        {
            e.ToTable("RoleClaim");
        });
        #endregion 
    }


}
