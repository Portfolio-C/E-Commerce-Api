using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.OrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/orderItems")]
    [ApiController]
    public class OrderItemsController(IOrderItemService service) : ControllerBase
    {
        /// <summary>
        /// Gets all Order items
        /// </summary>
        /// <returns>Returns list of Order items.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var orderItems = await service.GetAsync();

            return Ok(orderItems);
        }

        /// <summary>
        /// Gets an Order item by ID.
        /// </summary>
        /// <param name="id">Order item ID</param>
        /// <returns>Returns a single order item.</returns>
        [HttpGet("{id:int}", Name = nameof(GetOrderItemByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await service.GetByIdAsync(id);

            return Ok(orderItem);
        }

        /// <summary>
        /// Created an order item.
        /// </summary>
        /// <param name="request">An order item to create.</param>
        /// <returns>Returns newly created order item.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateOrderItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrderItem = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetOrderItemByIdAsync), new { id = createdOrderItem.Id }, createdOrderItem);
        }

        /// <summary>
        /// Update an order item.
        /// </summary>
        /// <param name="request">An order item to update.</param>
        /// <returns>Returns updated order item.</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateOrderItemRequest request)
        {
            var updatedOrderItem = await service.UpdateAsync(request);

            return Ok(updatedOrderItem);
        }

        /// <summary>
        /// Delete a single order item by ID.
        /// </summary>
        /// <param name="id">Order item ID to delete.</param>
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
