﻿using ECommerce.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ApplicationUserRole Role { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; }
    public virtual ICollection<Basket> Baskets { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

    public ApplicationUser()
    {
        Role = ApplicationUserRole.User;
    }
}
