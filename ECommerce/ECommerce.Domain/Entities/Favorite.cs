﻿using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ECommerce.Domain.Entities;

public class Favorite : EntityBase
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    public virtual ApplicationUser User { get; set; }
    public virtual Product Product { get; set; }
}
