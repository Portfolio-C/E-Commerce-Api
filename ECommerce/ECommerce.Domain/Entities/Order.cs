using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;
public class Order : EntityBase
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
}
