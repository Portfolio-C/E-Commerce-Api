using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Basket;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

internal sealed class BasketService(IApplicationDbContext context, IMapper mapper) : IBasketService
{
    public async Task<List<BasketDto>> GetAsync()
    {
        var baskets = await context.Baskets
        .ProjectTo<BasketDto>(mapper.ConfigurationProvider)
        .ToListAsync();

        return baskets;
    }

    public async Task<BasketDto> GetByIdAsync(int id)
    {
        var basket = await GetAndValidateBasketAsync(id);

        var dto = mapper.Map<BasketDto>(basket);

        return dto;
    }

    public async Task<BasketDto> CreateAsync(CreateBasketRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var newBasket = mapper.Map<Basket>(request);

        context.Baskets.Add(newBasket);
        await context.SaveChangesAsync();

        var basketDto = mapper.Map<BasketDto>(request);

        return basketDto;
    }

    public async Task<BasketDto> UpdateAsync(UpdateBasketRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!await context.Baskets.AnyAsync(b => b.Id == request.Id))
        {
            throw new EntityNotFoundException($"Basket with id: {request.Id} is not found");
        }

        var basketToUpdate = mapper.Map<Basket>(request);

        context.Baskets.Update(basketToUpdate);
        await context.SaveChangesAsync();

        var basketDto = mapper.Map<BasketDto>(request);

        return basketDto;
    }

    public async Task DeleteAsync(int id)
    {
        var basketToDelete = await GetAndValidateBasketAsync(id);

        context.Baskets.Remove(basketToDelete);
        await context.SaveChangesAsync();
    }

    private async Task<Basket> GetAndValidateBasketAsync(int id)
    {
        var entity = await context.Baskets.FirstOrDefaultAsync(b => b.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Basket with id: {id} is not found.");
        }

        return entity;
    }

}
