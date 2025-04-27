using System;

namespace ECommerce.Application.DTOs.OrderItem;

public sealed record OrderItemDto(
    int Id,
    int OrderId,
    int ProductId,
    int Quantity,
    decimal Price
);