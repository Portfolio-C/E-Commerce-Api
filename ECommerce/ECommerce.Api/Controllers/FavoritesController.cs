using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Favorite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Endpoints to manage favorites
    /// </summary>
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController(IFavoriteService service) : ControllerBase
    {
        /// <summary>
        /// Gets all favorites.
        /// </summary>
        /// <returns>Returns list of favorites.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var favroites = await service.GetAsync();

            return Ok(favroites);
        }

        /// <summary>
        /// Get a favorite by ID.
        /// </summary>
        /// <param name="id">Favorite ID.</param>
        /// <returns>Returns a single favorite.</returns>
        [HttpGet("{id:int}", Name = nameof(GetFavoritesByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavoritesByIdAsync(int id)
        {
            var favorite = await service.GetByIdAsync(id);

            return Ok(favorite);
        }

        /// <summary>
        /// Create a favorite
        /// </summary>
        /// <param name="request">A favorite to create.</param>
        /// <returns>Returns newly created favorite.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateFavoriteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFavorite = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetFavoritesByIdAsync), new { id = createdFavorite.Id }, createdFavorite);
        }

        /// <summary>
        /// Update a favorite.
        /// </summary>
        /// <param name="request">A favorite to update.</param>
        /// <returns>Returns updated favorite.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateFavoriteRequest request)
        {
            var updatedBasket = await service.UpdateAsync(request);

            return Ok(updatedBasket);
        }

        /// <summary>
        /// Delete a single favorite by ID.
        /// </summary>
        /// <param name="id">Favorite ID to delete.</param>
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }
    }
}
