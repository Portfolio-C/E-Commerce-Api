using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class OrderItem : EntityBase
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
