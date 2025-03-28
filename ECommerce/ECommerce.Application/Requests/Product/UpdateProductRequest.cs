using ECommerce.Application.DTOs.Attachment;

namespace ECommerce.Application.Requests.Product;

public sealed record UpdateProductRequest(
    int Id,
    int CategoryId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<AttachmentDto>? Attachments);
