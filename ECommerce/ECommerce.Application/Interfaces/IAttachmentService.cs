using ECommerce.Application.DTOs.Attachment;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces;

public interface IAttachmentService
{
    Task<List<AttachmentDto>> GetAsync();
    Task<AttachmentDto> GetByIdAsync(int id);
    Task<AttachmentDto> CreateAsync(IFormFile file, int productId);
    Task<AttachmentDto> UpdateAsync();
    Task DeleteAsync(int id);
}
