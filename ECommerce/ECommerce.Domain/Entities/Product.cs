using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Product : EntityBase
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;

    public virtual ICollection<Attachment> Attachments { get; set; }
    public virtual ICollection<Favorite> Favorites { get; set; }
    public virtual ICollection<Basket> Baskets { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public Product()
    {
        Attachments = new HashSet<Attachment>();
    }
}