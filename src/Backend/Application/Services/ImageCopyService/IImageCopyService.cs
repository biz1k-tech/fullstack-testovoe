using Application.Models;

namespace Application.Services.ImageCopyService;

public interface IImageCopyService
{
    Task<ImageCopyDto> CopyImageBaseAsync(Guid id, CancellationToken cancellationToken);
}