using ECommerce.Application.DTOs.Attachment;

namespace ECommerce.Application.DTOs.Product;

public sealed record ProductDto(
    int Id,
    int CategortId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<AttachmentDto> Attachments
    );
