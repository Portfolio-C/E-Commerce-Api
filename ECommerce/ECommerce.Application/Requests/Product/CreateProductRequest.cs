using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Requests.Product;

public sealed record CreateProductRequest(
    string Name,
    decimal Price,
    int Quantity,
    string? Description,
    int CategoryId,
    List<IFormFile> Images
    )
{
    public CreateProductRequest() : this("", 0m, 0, null, 0, new List<IFormFile>()) { }
}
