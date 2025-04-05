using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Requests.Attachment;

public sealed class CreateAttachmentRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be positive")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "File name is required")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "File name must be between 1 and 255 characters")]
    public string FileName { get; set; }

    [Required(ErrorMessage = "File type is required")]
    [StringLength(100, ErrorMessage = "File type must not exceed 100 characters")]
    public string FileType { get; set; }

    [Required(ErrorMessage = "File data is reqired")]
    public byte[] FileData { get; set; }

    public CreateAttachmentRequest(int productId, string fileName, string fileType, byte[] fileData)
    {
        ProductId = productId;
        FileName = fileName;
        FileType = fileType;
        FileData = fileData;
    }
}

