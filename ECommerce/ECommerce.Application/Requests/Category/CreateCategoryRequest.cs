using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Requests.Category;

public sealed record CreateCategoryRequest(
    [Required]
    [StringLength(100,MinimumLength =1)]
    string Name,

    [StringLength(500)]
    string? Description);
