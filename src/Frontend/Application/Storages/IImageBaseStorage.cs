using Domain.Entity;

namespace Application.Storages;

public interface IImageBaseStorage
{
    Task<bool> CreateImage(string description, Stream fileStream, string fileName, string contentType);
    Task<bool> UpdateImage(Guid id, string description);
    Task<bool> RemoveImage(Guid id);
    Task<bool> CopyImage(Guid id);
    Task<IEnumerable<ImageBase>> GetAllImages();
}