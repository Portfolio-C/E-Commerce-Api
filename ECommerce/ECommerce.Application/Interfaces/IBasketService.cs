using ECommerce.Application.DTOs.Basket;
using ECommerce.Application.Requests.Basket;

namespace ECommerce.Application.Interfaces;

public interface IBasketService
{
    Task<List<BasketDto>> GetAsync();
    Task<BasketDto> GetByIdAsync(int id);
    Task<BasketDto> CreateAsync(CreateBasketRequest request);
    Task<BasketDto> UpdateAsync(UpdateBasketRequest request);
    Task DeleteAsync(int id);
}
