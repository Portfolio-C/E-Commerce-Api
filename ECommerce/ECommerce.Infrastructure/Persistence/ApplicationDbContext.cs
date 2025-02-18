using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.Infrastructure.Persistence;

internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public virtual DbSet<ApplicationUser> ApplicationsUsers { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Attachment> Attachments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        #region Identity

        builder.Entity<ApplicationUser>(e =>
        {
            e.ToTable("User");
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

        builder.Entity<IdentityUserRole<string>>(e =>
        {
            e.ToTable("UserRole");
        });

        #endregion
    }
}