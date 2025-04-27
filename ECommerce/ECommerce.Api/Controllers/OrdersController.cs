using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Order;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Endpoint to manage Orders
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IOrderService service) : ControllerBase
    {
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>Returns list of Orders.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var orders = await service.GetAsync();

            return Ok(orders);
        }

        /// <summary>
        /// Get an order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Returns a single order</returns>
        [HttpGet("{id:int}", Name = nameof(GetOrderByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await service.GetByIdAsync(id);

            return Ok(order);
        }

        /// <summary>
        /// Create an order.
        /// </summary>
        /// <param name="request">An Order to create.</param>
        /// <returns>Returns newly created order.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
        {
            var createdOrder = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        /// <summary>
        /// Update an order.
        /// </summary>
        /// <param name="request">An order to update.</param>
        /// <returns>Returns updated order.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateOrderRequest request)
        {
            var updatedOrder = await service.UpdateAsync(request);

            return Ok(updatedOrder);
        }

        /// <summary>
        /// Delete a single order by ID.
        /// </summary>
        /// <param name="id">Order ID to delete.</param>
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
