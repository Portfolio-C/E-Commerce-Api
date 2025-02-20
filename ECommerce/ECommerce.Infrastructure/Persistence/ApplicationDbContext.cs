using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public virtual DbSet<ApplicationUser> ApplicationsUsers { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Attachment> Attachments { get; set; } = null!;
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

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