using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Requests.Order;

namespace ECommerce.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAsync();
    Task<OrderDto> GetByIdAsync(int id);
    Task<OrderDto> CreateAsync(CreateOrderRequest request);
    Task<OrderDto> UpdateAsync(UpdateOrderRequest request);
    Task DeleteAsync(int id);
}
