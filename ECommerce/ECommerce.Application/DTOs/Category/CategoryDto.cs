﻿namespace ECommerce.Application.DTOs.Category;

public sealed record CategoryDto(
    int Id,
    string Name,
    string? Description);