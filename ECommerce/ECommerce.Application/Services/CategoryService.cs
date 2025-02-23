using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using ECommerce.Application.DTOs.Category;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Category;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

internal sealed class CategoryService(IApplicationDbContext context, IMapper mapper) : ICateogryService
{
    public async Task<List<CategoryDto>> GetAsync()
    {
        var categories = await context.Categories
            .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return categories;  
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await GetAndValidateCategoryAsync(id);

        var dto = mapper.Map<CategoryDto>(category);

        return dto;
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newCategory = mapper.Map<Category>(request);

        context.Categories.Add(newCategory);
        await context.SaveChangesAsync();

        var categoryDto = mapper.Map<CategoryDto>(newCategory);

        return categoryDto;
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!await context.Categories.AnyAsync(c => c.Id == request.Id))
        {
            throw new EntityNotFoundException($"Category with id: {request.Id} is not found");
        }

        var categoryToUpdate = mapper.Map<Category>(request);

        context.Categories.Update(categoryToUpdate);
        await context.SaveChangesAsync();

        var categoryDto = mapper.Map<CategoryDto>(categoryToUpdate);

        return categoryDto;
    }

    public async Task DeleteAsync(int id)
    {
        var categoryToDelete = await GetAndValidateCategoryAsync(id);

        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();
    }

    private async Task<Category> GetAndValidateCategoryAsync(int id)
    {
        var entity = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Category with id: {id} is not found");
        }

        return entity;
    }
}
