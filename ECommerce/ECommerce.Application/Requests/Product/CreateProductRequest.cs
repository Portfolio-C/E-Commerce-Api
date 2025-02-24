using System.Net.Mail;

namespace ECommerce.Application.Requests.Product;

public sealed record CreateProductRequest(
    int CategoryId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<Attachment> Attachments);
