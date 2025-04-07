﻿using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Basket : EntityBase
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    public virtual ApplicationUser User { get; set; }
    public virtual Product Product { get; set; }
}
