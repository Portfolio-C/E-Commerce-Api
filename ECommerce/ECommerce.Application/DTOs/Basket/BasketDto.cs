namespace ECommerce.Application.DTOs.Basket;

public sealed record BasketDto(
    int Id,
    string UserId,
    int ProductId,
    int Quantity,
    DateTime Date
    )
{
    public BasketDto() : this(0, "", 0, 0, DateTime.Now)
    {

    }
}
