using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;
public class Attachment : EntityBase
{
    public required byte[] Image { get; set; }

    public int ProductId { get; set; }
    public required virtual Product Product { get; set; }
}
