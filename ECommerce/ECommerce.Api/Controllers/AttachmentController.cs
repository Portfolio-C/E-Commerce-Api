using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Attachment;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/attachments")]
[ApiController]
public class AttachmentController(IAttachmentService service) : Controller
{
    /// <summary>
    /// Gets all attachments.
    /// </summary>
    /// <returns>Returns list of attachments.</returns>
    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var attachments = await service.GetAsync();

            return Ok(attachments);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Gets a attachment by ID.
    /// </summary>
    /// <param name="id">Attachment ID.</param>
    /// <returns>Returns a single Attachment.</returns>
    [HttpGet("{id:int}", Name = nameof(GetAttachmentByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAttachmentByIdAsync(int id)
    {
        try
        {
            var attachment = await service.GetByIdAsync(id);

            return Ok(attachment);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Create a attachment.
    /// </summary>
    /// <returns>Returns newly created attachment.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadAttachment([FromForm] CreateAttachmentRequest request)
    {
        try
        {
            var createdAttachment = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetAttachmentByIdAsync), new { id = createdAttachment.Id }, createdAttachment);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a single attachment by Id.
    /// </summary>
    /// <param name="id">Attachment Id to delete.</param>
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
}