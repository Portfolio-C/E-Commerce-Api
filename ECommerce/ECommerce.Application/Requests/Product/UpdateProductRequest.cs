using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Requests.Product;

public sealed record UpdateProductRequest(
    int Id,
    int CategoryId,
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    List<IFormFile> Images);
