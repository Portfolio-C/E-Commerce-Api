using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.Order;

public sealed record OrderDto(
    int Id,
    string UserId,
    DateTime OrderDate,
    decimal TotalAmount,
    OrderStatus Status,
    IEnumerable<OrderItemDto> Items);
