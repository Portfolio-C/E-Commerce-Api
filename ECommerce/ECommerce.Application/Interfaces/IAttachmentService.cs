using ECommerce.Application.DTOs.Attachment;
using ECommerce.Application.Requests.Attachment;

namespace ECommerce.Application.Interfaces;

public interface IAttachmentService
{
    Task<List<AttachmentDto>> GetAsync();
    Task<AttachmentDto> GetByIdAsync(int id);
    Task<AttachmentDto> CreateAsync(CreateAttachmentRequest request);
    Task<AttachmentDto> UpdateAsync();
    Task DeleteAsync(int id);
}
