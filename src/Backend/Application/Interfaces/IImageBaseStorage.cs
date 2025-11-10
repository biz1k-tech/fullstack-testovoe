using Domain.Entity;

namespace Application.Interfaces;

public interface IImageBaseStorage : IStorage
{
    Task<ImageBase> GetImageAsync(Guid id, CancellationToken cancellationToken);

    Task<ImageBase> CreateImageAsync(Guid id, string description, string filePath,
        CancellationToken cancellationToken);

    Task UpdateImageAsync(Guid id, string description, CancellationToken cancellationToken);
    Task<IEnumerable<ImageBase>> GetAllImagesAsync(CancellationToken cancellationToken);
    Task SetImageCopiedFlagAsync(Guid id, bool flag, CancellationToken cancellationToken);
    Task RemoveImageAsync(Guid id, CancellationToken cancellationToken);
}