using Domain.Entity;

namespace Application.Interfaces;

public interface IImageCopyStorage : IStorage
{
    /// <param name="id">Нужен imageBase.Id </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ImageCopy> GetImageAsync(Guid id, CancellationToken cancellationToken);

    Task RemoveImageAsync(Guid id, CancellationToken cancellationToken);

    Task<ImageCopy> CopyImageBaseAsync(Guid id, string sourceDescription,
        byte[] blob, CancellationToken cancellationToken);

    Task UpdateImageCopyAsync(Guid id, string description, CancellationToken cancellationToken);
}