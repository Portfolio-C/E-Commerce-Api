using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Product;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

internal sealed class ProductService(IApplicationDbContext context, IMapper mapper) : IProductService
{
    public async Task<List<ProductDto>> GetAsync()
    {
        var products = await context.Products
            .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return products;
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await GetAndValidateProductAsync(id);

        var dto = mapper.Map<ProductDto>(product);

        return dto;
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newProduct = mapper.Map<Product>(request);

        if (request.Images is not null && request.Images.Any())
        {
            foreach (var image in request.Images)
            {
                if (image.Length > 0)
                {
                    try
                    {
                        using var memoryStream = new MemoryStream();
                        await image.CopyToAsync(memoryStream);
                        var fileData = memoryStream.ToArray();

                        var attachment = new Attachment
                        {
                            FileName = image.FileName,
                            FileType = image.ContentType,
                            FileData = fileData,
                            Product = newProduct
                        };

                        context.Attachments.Add(attachment);

                        newProduct.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Error processing image: {ex.Message}", ex);
                    }
                }
            }
        }

        try
        {
            context.Products.Add(newProduct);
            await context.SaveChangesAsync();
            var productDto = mapper.Map<ProductDto>(newProduct);

            return productDto;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error saving to database: {ex.Message}", ex);
        }

    }

    public async Task<ProductDto> UpdateAsync(UpdateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!await context.Products.AnyAsync(x => x.Id == request.Id))
        {
            throw new EntityNotFoundException($"Product with id: {request.Id} is not found.");
        }

        var productToUpdate = mapper.Map<Product>(request);

        context.Products.Update(productToUpdate);
        await context.SaveChangesAsync();

        var productDto = mapper.Map<ProductDto>(productToUpdate);

        return productDto;
    }

    public async Task DeleteAsync(int id)
    {
        var productToDelete = await GetAndValidateProductAsync(id);

        context.Products.Remove(productToDelete);
        await context.SaveChangesAsync();
    }

    private async Task<Product> GetAndValidateProductAsync(int id)
    {
        var entity = await context.Products
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Product with id: {id} not found.");
        }

        return entity;
    }
}