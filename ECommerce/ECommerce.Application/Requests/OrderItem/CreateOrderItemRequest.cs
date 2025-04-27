using System;

namespace ECommerce.Application.Requests.OrderItem;

public sealed record CreateOrderItemRequest(
    int ProductId,
    int Quantity,
    decimal Price
);
