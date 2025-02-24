using System.Net.Mail;

namespace ECommerce.Application.DTOs.Product;

public sealed record ProductDto(
    int Id,
    int CategortId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<Attachment> Attachments
    );
