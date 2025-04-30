using System;
using AutoMapper;
using ECommerce.Application.DTOs.Favorite;
using ECommerce.Application.Requests.Favorite;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class FavoriteMappings : Profile
{
    public FavoriteMappings()
    {
        CreateMap<Favorite, FavoriteDto>();
        CreateMap<FavoriteDto, CreateFavoriteRequest>();
        CreateMap<FavoriteDto, UpdateFavoriteRequest>();
    }
}
