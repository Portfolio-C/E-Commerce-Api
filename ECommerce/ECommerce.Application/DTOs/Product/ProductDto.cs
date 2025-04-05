using ECommerce.Application.DTOs.Attachment;

namespace ECommerce.Application.DTOs.Product;

public sealed record ProductDto(
    int Id,
    int CategoryId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<AttachmentDto> Attachments
    )
{
    public ProductDto() : this(0, 0, "", 0m, 0, null, new List<AttachmentDto>())
    {

    }
}
