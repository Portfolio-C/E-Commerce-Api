using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IProductService service) : Controller
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>Returns list of products.</returns>
    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        var products = await service.GetAsync();

        return Ok(products);
    }

    /// <summary>
    /// Gets a product by ID.
    /// </summary>
    /// <param name="id">Product Id.</param>
    /// <returns>Returns a single product.</returns>
    [HttpGet("{id:int}", Name = nameof(GetProductByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductByIdAsync([FromQuery] int id)
    {
        var product = await service.GetByIdAsync(id);

        return Ok(product);
    }

    /// <summary>
    /// Create a product.
    /// </summary>
    /// <param name="requst">A product to create.</param>
    /// <returns>Returns newly created product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(CreateProductRequest requst)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProduct = await service.CreateAsync(requst);

        return CreatedAtAction(nameof(GetProductByIdAsync), new { id = createdProduct.Id }, createdProduct);
    }

    /// <summary>
    /// Updates a product.
    /// </summary>
    /// <param name="request">A product to update.</param>
    /// <returns>Returns updated product.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest request)
    {
        var updatedProduct = await service.UpdateAsync(request);

        return Ok(updatedProduct);
    }

    /// <summary>
    /// Delete a single product by Id.
    /// </summary>
    /// <param name="id">Product Id to delete.</param>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        await service.DeleteAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Gets allowed methods for this resource.
    /// </summary>
    /// <returns>Allowed methods for this resource.</returns>
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
