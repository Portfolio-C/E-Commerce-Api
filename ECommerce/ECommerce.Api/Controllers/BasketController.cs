using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Basket;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

/// <summary>
/// Endpoints to manage baskets
/// </summary>
[Route("api/baskets")]
[ApiController]
public class BasketController(IBasketService service) : ControllerBase
{
    /// <summary>
    /// Gets all baskets
    /// </summary>
    /// <returns> Returns list of baskets.</returns>
    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        var basketes = await service.GetAsync();

        return Ok(basketes);
    }

    /// <summary>
    /// Gets a basket by ID.
    /// </summary>
    /// <param name="id">Basket ID.</param>
    /// <returns>Returns a single basket.</returns>
    [HttpGet("{id:int}", Name = nameof(GetBasketByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBasketByIdAsync([FromQuery] int id)
    {
        var basket = await service.GetByIdAsync(id);

        return Ok(basket);
    }

    /// <summary>
    /// Create a basket
    /// </summary>
    /// <param name="request">A basket to create.</param>
    /// <returns>Returns newly created basket.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBasketRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdBasket = await service.CreateAsync(request);

        return CreatedAtAction(nameof(GetBasketByIdAsync), new { id = createdBasket.Id }, createdBasket);
    }

    /// <summary>
    /// Updates a basket
    /// </summary>
    /// <param name="request">A basket to update.</param>
    /// <returns>Returns updated basket.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(UpdateBasketRequest request)
    {
        var updatedBasket = await service.UpdateAsync(request);

        return Ok(updatedBasket);
    }
    /// <summary>
    /// Delete a single basket by ID.
    /// </summary>
    /// <param name="id">Basket ID to delete.</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await service.DeleteAsync(id);

        return NoContent();
    }
}
