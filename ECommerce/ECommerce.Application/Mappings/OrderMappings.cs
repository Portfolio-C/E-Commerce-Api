using System;
using AutoMapper;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Requests.Order;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class OrderMappings : Profile
{
    public OrderMappings()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderRequest, OrderDto>();
        CreateMap<UpdateOrderRequest, OrderDto>();
    }
}
