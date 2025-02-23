using AutoMapper;
using ECommerce.Application.DTOs.Category;
using ECommerce.Application.Requests.Category;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
    }
}