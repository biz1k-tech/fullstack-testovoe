using Application.Models.Enum;

namespace Application.Interfaces
{
    public interface IFileSystemImageStorage 
    {
        Task<byte[]> ReadImageAsBytesAsync(string fileName,
            CancellationToken cancellationToken);
        Task RemoveImageAsync(string filePath, CancellationToken cancellationToken);
        Task<bool> CheckFileExistsAsync(string filePath, CancellationToken cancellationToken);

        Task<(string FullImagePath, string ThumbnailPath)> SaveImageWithResizeAsync(
            Guid id, Stream originalStream, Stream thumbnailStream, string extension,
            CancellationToken cancellationToken);
    }
}
