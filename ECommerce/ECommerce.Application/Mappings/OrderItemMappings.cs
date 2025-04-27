using System;
using AutoMapper;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Application.Requests.OrderItem;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class OrderItemMappings : Profile
{
    public OrderItemMappings()
    {
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderItemRequest, OrderItem>();
        CreateMap<UpdateOrderItemRequest, OrderItem>();
    }
}
