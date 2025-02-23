namespace ECommerce.Application.Requests.Category;

public sealed record CreateCategoryRequest(string Name, string? Description);
