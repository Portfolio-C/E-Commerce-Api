using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Requests.Attachment;

public sealed record CreateAttachmentRequest(
    [Range(1,int.MaxValue,ErrorMessage ="ProductId must be positive")]
    int ProductId,

    [Required(ErrorMessage ="File name is required")]
    [StringLength(255,MinimumLength =1,ErrorMessage ="File name must be between 1 and 255 characters")]
    string FileName,

    [Required(ErrorMessage ="File type is required")]
    [StringLength(100,ErrorMessage ="File type must not exceed 100 characters")]
    string FileType,

    [Required(ErrorMessage ="File data is reqired")]
    byte[] FileData
    );

