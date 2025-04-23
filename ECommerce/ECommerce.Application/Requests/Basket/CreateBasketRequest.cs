namespace ECommerce.Application.Requests.Basket;

public sealed record CreateBasketRequest(
    int Quantity,
    int ProductId
);