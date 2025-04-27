using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.OrderItem;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

public class OrderItemService(IApplicationDbContext context, IMapper mapper) : IOrderItemService
{
    public async Task<List<OrderItemDto>> GetAsync()
    {
        var orderItems = await context.OrderItems
            .ProjectTo<OrderItemDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return orderItems;
    }

    public async Task<OrderItemDto> GetByIdAsync(int id)
    {
        var orderItem = await GetAndValidateOrderItemAsync(id);

        var dto = mapper.Map<OrderItemDto>(orderItem);

        return dto;
    }

    public async Task<OrderItemDto> CreateAsync(CreateOrderItemRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newOrderItem = mapper.Map<OrderItem>(request);

        context.OrderItems.Add(newOrderItem);
        await context.SaveChangesAsync();

        var orderItemDto = mapper.Map<OrderItemDto>(newOrderItem);

        return orderItemDto;
    }

    public async Task<OrderItemDto> UpdateAsync(UpdateOrderItemRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!await context.OrderItems.AnyAsync(o => o.Id == request.Id))
        {
            throw new EntityNotFoundException($"OrderItem with id: {request.Id} is not found");
        }

        var orderItemToUpdate = mapper.Map<OrderItem>(request);

        context.OrderItems.Update(orderItemToUpdate);
        await context.SaveChangesAsync();

        var orderItemDto = mapper.Map<OrderItemDto>(orderItemToUpdate);

        return orderItemDto;
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await GetAndValidateOrderItemAsync(id);

        context.OrderItems.Remove(orderItem);
        await context.SaveChangesAsync();
    }

    private async Task<OrderItem> GetAndValidateOrderItemAsync(int id)
    {
        var orderItem = await context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);

        if (orderItem is null)
        {
            throw new EntityNotFoundException($"OrderItem with id: {id} is not found.");
        }

        return orderItem;
    }
}