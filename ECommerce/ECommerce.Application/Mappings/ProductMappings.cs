using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Requests.Product;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}
