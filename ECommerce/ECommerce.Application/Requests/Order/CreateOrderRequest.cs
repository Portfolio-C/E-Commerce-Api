using System;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Requests.Order;

public sealed record CreateOrderRequest(
    string UserId,
    decimal TotalAmount,
    OrderStatus Status,
    IEnumerable<OrderItemDto> Items
    );
