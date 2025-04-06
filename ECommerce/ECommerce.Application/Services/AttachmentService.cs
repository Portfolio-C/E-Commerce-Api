using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs.Attachment;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Attachment;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

internal sealed class AttachmentService(
    IApplicationDbContext context,
    IMapper mappaer,
    IProductService productService) : IAttachmentService
{
    public async Task<List<AttachmentDto>> GetAsync()
    {
        var attachments = await context.Attachments
            .ProjectTo<AttachmentDto>(mappaer.ConfigurationProvider)
            .ToListAsync();

        return attachments;
    }

    public async Task<AttachmentDto> GetByIdAsync(int id)
    {
        var attachment = await GetAndValidateAttachmentAsync(id);

        var dto = mappaer.Map<AttachmentDto>(attachment);

        return dto;
    }

    public async Task<AttachmentDto> CreateAsync(CreateAttachmentRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var product = await productService.GetByIdAsync(request.ProductId);

        ArgumentNullException.ThrowIfNull(product);

        using var memoryStream = new MemoryStream();


        //var newAttachment = new CreateAttachmentRequest(request.ProductId, file.FileName, file.ContentType, memoryStream.ToArray());

        var entity = mappaer.Map<Attachment>(null);

        context.Attachments.Add(entity);
        await context.SaveChangesAsync();

        var attachmentDto = mappaer.Map<AttachmentDto>(null);

        return attachmentDto;
    }

    public Task<AttachmentDto> UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        var attachment = await GetAndValidateAttachmentAsync(id);

        context.Attachments.Remove(attachment);
        await context.SaveChangesAsync();
    }

    private async Task<Attachment> GetAndValidateAttachmentAsync(int id)
    {
        var attachment = await context.Attachments.FirstOrDefaultAsync(x => x.Id == id);

        if (attachment is null)
        {
            throw new EntityNotFoundException($"Attachment with id: {id} is not found.");
        }

        return attachment;
    }
}
