using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Application.Requests.OrderItem;

namespace ECommerce.Application.Interfaces;

public interface IOrderItemService
{
    Task<List<OrderItemDto>> GetAsync();
    Task<OrderItemDto> GetByIdAsync(int id);
    Task<OrderItemDto> CreateAsync(CreateOrderItemRequest request);
    Task<OrderItemDto> UpdateAsync(UpdateOrderItemRequest request);
    Task DeleteAsync(int id);
}
