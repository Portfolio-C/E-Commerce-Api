using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Category;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Api.Controllers;

[Route("api/categories")]
[ApiController]

public class CategoriesController(ICateogryService service) : Controller
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>Returns list of categories.</returns>
    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var categories = await service.GetAsync();

            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Gets a category by ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <returns>Returns a single category.</returns>
    [HttpGet("{id:int}", Name = "GetByIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        try
        {
            var category = await service.GetByIdAsync(id);

            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Creates a category.
    /// </summary>
    /// <param name="request">A category to create.</param>
    /// <returns>Returns newly created category.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request)
    {
        try
        {
            var newCategory = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newCategory.Id }, newCategory);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="request">A category to update.</param>
    /// <returns>Returns updated category.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryRequest request)
    {
        try
        {
            var updatedCategory = await service.UpdateAsync(request);

            return Ok(updatedCategory);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a single category.
    /// </summary>
    /// <param name="id">Category ID to delete.</param>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await service.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Gets allowed methods for this resource.
    /// </summary>
    /// <returns>Allowed methods for this resource</returns>
    [HttpOptions]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetOptions()
    {
        string[] options = ["GET", "POST", "PUT", "DELETE", "HEAD", "PATCH"];

        HttpContext.Response.Headers.Append("X-Options", JsonConvert.SerializeObject(options));

        return Ok(options);
    }
}