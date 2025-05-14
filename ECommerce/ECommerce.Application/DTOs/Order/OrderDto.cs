using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.Order;

public sealed record OrderDto(
    int Id,
    string UserId,
    DateTime OrderDate,
    decimal TotalAmount,
    OrderStatus Status,
    IEnumerable<OrderItemDto> Items)
{
    public OrderDto() : this(0, "", DateTime.Now, 0m, OrderStatus.Pending, new List<OrderItemDto>())
    {

    }
}
