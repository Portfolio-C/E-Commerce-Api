using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Requests.Product;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Mappings;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => MapImagesToAttachments(src.Images)));
    }

    private static ICollection<Attachment> MapImagesToAttachments(ICollection<IFormFile> images)
    {
        if (images == null || images.Count == 0)
            return new List<Attachment>();

        var attachments = new List<Attachment>();
        foreach (var image in images)
        {
            if (image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                var fileData = memoryStream.ToArray();
                var attachment = new Attachment
                {
                    FileName = image.FileName,
                    FileType = image.ContentType,
                    FileData = fileData
                };
                attachments.Add(attachment);
            }
        }
        return attachments;
    }
}
