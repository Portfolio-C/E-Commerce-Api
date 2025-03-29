namespace ECommerce.Application.DTOs.Attachment;

public sealed record AttachmentDto(
    int Id,
    string FileName,
    string FileType
    );
