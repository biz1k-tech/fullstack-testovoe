using Application.Configuration;
using Application.Interfaces;
using Application.Models.Enum;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Common.Storages
{
    public class FileSystemImageStorage : IFileSystemImageStorage
    {
        private readonly string _imagesPath;
        private readonly string _configuredPath;
        private readonly string _requestPath;
        private readonly IOptions<ImagePathOptions> _options;

        public FileSystemImageStorage(
            IOptions<ImagePathOptions> options)
        {
            _options = options;
            _configuredPath = _options.Value.Path;
            _requestPath = _options.Value.RequestPath;

            var basePath = Directory.GetCurrentDirectory();
            _imagesPath = Path.GetFullPath(Path.Combine(basePath, _configuredPath));

            Directory.CreateDirectory(_imagesPath);
        }

        public async Task<byte[]?> ReadImageAsBytesAsync(string filePath,
            CancellationToken cancellationToken)
        {
            var fullPath = GetFullPathImage(filePath);

            return await File.ReadAllBytesAsync(fullPath, cancellationToken);
        }

        public Task RemoveImageAsync(string filePath, CancellationToken cancellationToken)
        {
            var fullPath = GetFullPathImage(filePath);

            var searchDirectory = Path.GetDirectoryName(fullPath);
            var nameWithoutExt = Path.GetFileNameWithoutExtension(filePath);

            var suffix = $"-{(ImageSuffix.Original).ToString().ToLowerInvariant()}";
            var baseId = nameWithoutExt.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? nameWithoutExt.Substring(0, nameWithoutExt.Length - suffix.Length)
                : nameWithoutExt;

            foreach (var file in Directory.EnumerateFiles(searchDirectory, $"{baseId}*", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }

            return Task.CompletedTask;
        }

        public Task<bool> CheckFileExistsAsync(string filePath, CancellationToken cancellationToken)
        {
            var fullPath = GetFullPathImage(filePath);

            bool exists = File.Exists(fullPath);

            return Task.FromResult(exists);
        }

        public async Task<(string FullImagePath, string ThumbnailPath)> SaveImageWithResizeAsync(Guid id,
            Stream originalStream,
            Stream thumbnailStream, string extension,
            CancellationToken cancellationToken)
        {
            using var fullImageTask = SaveStreamToFileAsync(id, ImageSuffix.Original, originalStream, extension, cancellationToken);
            using var thumbnailTask = SaveStreamToFileAsync(id, ImageSuffix.Resize, thumbnailStream, extension, cancellationToken);

            await Task.WhenAll(fullImageTask, thumbnailTask);

            return (
                FullImagePath: fullImageTask.Result,
                ThumbnailPath: thumbnailTask.Result
            );
        }

        #region private

        private async Task<string> SaveStreamToFileAsync(
            Guid id, ImageSuffix suffix, Stream imageStream, string extension,
            CancellationToken cancellationToken)
        {
            var fileName = $"{id}-{suffix.ToString().ToLowerInvariant()}{extension}";
            var filePath = Path.Combine(_configuredPath, fileName).Replace("\\", "/");
            var relativePath = Path.Combine(_requestPath, fileName).Replace("\\", "/");

            await using var file = File.Create(filePath);
            await imageStream.CopyToAsync(file, cancellationToken);

            return relativePath;
        }
        private string GetFullPathImage(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            var fullPath = Path.Combine(_imagesPath, fileName);

            return fullPath;
        }
        #endregion 
    }
}
