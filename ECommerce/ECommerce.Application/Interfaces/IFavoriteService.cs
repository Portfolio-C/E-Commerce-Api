using System;
using ECommerce.Application.DTOs.Favorite;
using ECommerce.Application.Requests.Favorite;

namespace ECommerce.Application.Interfaces;

public interface IFavoriteService
{
    Task<List<FavoriteDto>> GetAsync();
    Task<FavoriteDto> GetByIdAsync(int id);
    Task<FavoriteDto> CreateAsync(CreateFavoriteRequest request);
    Task<FavoriteDto> UpdateAsync(UpdateFavoriteRequest request);
    Task DeleteAsync(int id);
}
