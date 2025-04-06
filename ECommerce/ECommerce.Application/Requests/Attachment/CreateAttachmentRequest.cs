using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Requests.Attachment;

public sealed record CreateAttachmentRequest(
    string FileName,
    string FileType,
    int ProductId,
    List<IFormFile> Images);

