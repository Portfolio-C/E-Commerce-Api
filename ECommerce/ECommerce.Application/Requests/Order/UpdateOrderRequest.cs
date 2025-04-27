using System;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Requests.Order;

public sealed record UpdateOrderRequest(
    int Id,
    decimal TotalAmount,
    OrderStatus Status,
    List<OrderItemDto> Items
);