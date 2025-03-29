using ECommerce.Application.DTOs.Attachment;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Requests.Product;

public sealed record CreateProductRequest(
    [Range(1,int.MaxValue)]
    int CategoryId,

    [Required]
    string Name,

    [Range(0.01,999999.99,ErrorMessage ="Price must be between 0.01 and 999999.99")]
    decimal Price,

    [Range(1,int.MaxValue)]
    int Quantity,

    [StringLength(1000)]
    string? Description,
    List<AttachmentDto>? AttachmentIds
    );
