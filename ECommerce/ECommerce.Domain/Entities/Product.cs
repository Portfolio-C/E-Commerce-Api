using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;
public class Product : EntityBase
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }

    public int CategoryId { get; set; }
    public required virtual Category Category { get; set; }
    public required virtual ICollection<Attachment> Attachments { get; set; }
}
