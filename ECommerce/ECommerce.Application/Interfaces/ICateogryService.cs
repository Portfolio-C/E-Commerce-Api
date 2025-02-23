using ECommerce.Application.DTOs.Category;
using ECommerce.Application.Requests.Category;

namespace ECommerce.Application.Interfaces;

public interface ICateogryService
{
    Task<List<CategoryDto>> GetAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CreateCategoryRequest request);
    Task<CategoryDto> UpdateAsync(UpdateCategoryRequest request);
    Task DeleteAsync(int id);
}
