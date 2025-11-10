using Application.Models;

namespace Application.Services.ImageBaseService
{
    public interface IImageBaseService
    {
        Task<ImageBaseDto> CreateImageAsync(CreateImageDto imageDto, CancellationToken cancellationToken);
        Task UpdateImageAsync(UpdateImageDto imageDto, CancellationToken cancellationToken);
        Task<IEnumerable<ImageBaseViewDto>> GetAllImagesAsync(CancellationToken cancellationToken);
        Task RemoveImageAsync(Guid id, CancellationToken cancellationToken);
    }
}
