using AutoMapper;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Application.Requests.Basket;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class BasketMappings : Profile
{
    public BasketMappings()
    {
        CreateMap<Basket, BasketDto>();
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<UpdateBasketRequest, Basket>();
    }
}
