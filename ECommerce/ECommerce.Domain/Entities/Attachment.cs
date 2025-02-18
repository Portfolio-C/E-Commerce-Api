using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Attachment : EntityBase
{
    public required string FileName { get; set; }
    public required string FileType { get; set; }
    public required byte[] FileData { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}
