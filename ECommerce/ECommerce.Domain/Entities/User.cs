using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities;
public class User : EntityBase
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; }

    public required string AccountId { get; set; }
    public required virtual IdentityUser Account { get; set; }
}
