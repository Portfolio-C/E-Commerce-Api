namespace ECommerce.Application.Requests.Basket;

public sealed record UpdateBasketRequest(
    int Id,
    int Quantity);
