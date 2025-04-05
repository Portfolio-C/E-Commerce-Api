using AutoMapper;
using ECommerce.Application.DTOs.Attachment;
using ECommerce.Application.Requests.Attachment;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;
public sealed class AttachmentMappings : Profile
{
    public AttachmentMappings()
    {
        CreateMap<Attachment, AttachmentDto>();
        CreateMap<CreateAttachmentRequest, AttachmentDto>();
    }
}
