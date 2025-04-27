using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Order;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

public class OrderService(IApplicationDbContext context, IMapper mapper) : IOrderService
{
    public async Task<List<OrderDto>> GetAsync()
    {
        var orders = await context.Orders
            .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return orders;
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var order = await GetAndValidateOrderAsync(id);

        var orderDto = mapper.Map<OrderDto>(order);

        return orderDto;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newOrder = mapper.Map<Order>(request);

        context.Orders.Add(newOrder);
        await context.SaveChangesAsync();

        var orderDto = mapper.Map<OrderDto>(newOrder);

        return orderDto;
    }

    public async Task<OrderDto> UpdateAsync(UpdateOrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var orderToUpdate = mapper.Map<Order>(request);

        context.Orders.Update(orderToUpdate);
        await context.SaveChangesAsync();

        var orderDto = mapper.Map<OrderDto>(request);

        return orderDto;
    }

    public async Task DeleteAsync(int id)
    {
        var order = await GetAndValidateOrderAsync(id);

        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }

    private async Task<Order> GetAndValidateOrderAsync(int id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
        {
            throw new EntityNotFoundException($"Order with id: {id} is not found");
        }

        return order;
    }

}
