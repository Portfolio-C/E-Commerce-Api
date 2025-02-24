using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Requests.Product;

namespace ECommerce.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(CreateProductRequest request);
    Task<ProductDto> UpdateAsync(UpdateProductRequest request);
    Task DeleteAsync(int id);
}
