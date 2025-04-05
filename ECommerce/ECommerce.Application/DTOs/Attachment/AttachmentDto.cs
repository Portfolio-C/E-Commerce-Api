namespace ECommerce.Application.DTOs.Attachment;

public sealed record AttachmentDto(
    int Id,
    int ProductId,
    string FileName,
    string FileType
    );
