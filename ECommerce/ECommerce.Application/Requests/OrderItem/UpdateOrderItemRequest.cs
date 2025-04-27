using System;

namespace ECommerce.Application.Requests.OrderItem;

public sealed record UpdateOrderItemRequest(
    int Id,
    int ProductId,
    int Quantity,
    decimal Price
);
