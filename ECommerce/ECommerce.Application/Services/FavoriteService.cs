using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.Favorite;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Favorite;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ECommerce.Application.Services;

public class FavoriteService(IApplicationDbContext context, IMapper mapper) : IFavoriteService
{
    public async Task<List<FavoriteDto>> GetAsync()
    {
        var favorites = await context.Favorites
            .ProjectTo<FavoriteDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return favorites;
    }

    public async Task<FavoriteDto> GetByIdAsync(int id)
    {
        var favorite = await GetAndValidateFavoriteAsync(id);

        var dto = mapper.Map<FavoriteDto>(favorite);

        return dto;
    }

    public async Task<FavoriteDto> CreateAsync(CreateFavoriteRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newFavorite = mapper.Map<Favorite>(request);

        context.Favorites.Add(newFavorite);
        await context.SaveChangesAsync();

        var favoriteDto = mapper.Map<FavoriteDto>(newFavorite);

        return favoriteDto;
    }

    public async Task<FavoriteDto> UpdateAsync(UpdateFavoriteRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var favortieToUpdate = mapper.Map<Favorite>(request);

        context.Favorites.Update(favortieToUpdate);
        await context.SaveChangesAsync();

        var favoriteDto = mapper.Map<FavoriteDto>(favortieToUpdate);

        return favoriteDto;
    }

    public async Task DeleteAsync(int id)
    {
        var favorite = await GetAndValidateFavoriteAsync(id);

        context.Favorites.Remove(favorite);
        await context.SaveChangesAsync();
    }

    private async Task<Favorite> GetAndValidateFavoriteAsync(int id)
    {
        var favorite = await context.Favorites.FirstOrDefaultAsync(x => x.Id == id);

        if (favorite is null)
        {
            throw new EntityNotFoundException($"Favorite with id: {id} is not found.");
        }

        return favorite;
    }
}
