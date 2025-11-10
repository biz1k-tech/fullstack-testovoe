using Application.Models;

namespace Application.Services.ImageBaseService;

public interface IImageBaseService
{
    Task<bool> CreateImageAsync(CreateImageDto imageDto);
    Task<bool> UpdateImage(UpdateImageDto imageDto);
    Task<bool> RemoveImage(Guid id);
    Task<bool> CopyImage(Guid id);
    Task<IEnumerable<ImageBaseDto>?> GetAllImagesAsync();
}