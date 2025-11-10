using Application.Configuration;
using Application.Interfaces;
using Application.Models;
using Application.Models.Enum;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Microsoft.Extensions.Options;

namespace Application.Services.ImageBaseService
{
    public class ImageBaseService : IImageBaseService
    {
        private readonly IImageBaseStorage _imageBaseStorage;
        private readonly IImageCopyStorage _imageCopyStorage;
        private readonly IFileSystemImageStorage _fileSystemImageStorage;
        private readonly IImageResize _imageResize;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkBase1 _unitOfWork;
        private readonly IOptions<ImageResizeOptions> _options;

        public ImageBaseService(
            IImageBaseStorage imageBaseStorage,
            IImageCopyStorage imageCopyStorage,
            IFileSystemImageStorage fileSystemImageStorage,
            IUnitOfWorkBase1 unitOfWork,
            IImageResize imageResize,
            IOptions<ImageResizeOptions> options,
            IMapper mapper)
        {
            _mapper = mapper;
            _imageCopyStorage = imageCopyStorage;
            _imageBaseStorage = imageBaseStorage;
            _fileSystemImageStorage = fileSystemImageStorage;
            _imageResize = imageResize;
            _unitOfWork = unitOfWork;
            _options = options;
        }
        public async Task<ImageBaseDto> CreateImageAsync(
            CreateImageDto imageDto, CancellationToken cancellationToken)
        {
            //Создается тут, потому что нужно, чтобы фактический Id был в название картинки
            Guid imageId = Guid.NewGuid();

            using var stream = imageDto.FileStream;

            var resize = _imageResize.Resize(stream, _options.Value);
            var filePath = await _fileSystemImageStorage.SaveImageWithResizeAsync(
                imageId, resize.FullImageStream, resize.ThumbnailStream, imageDto.Extension, cancellationToken);

            ImageBase image=null;
            try
            {
                await using (var scope = await _unitOfWork.StartScope(cancellationToken))
                {
                    var scopedImageBaseStorage = scope.GetStorage<IImageBaseStorage>();
                    image = await scopedImageBaseStorage.CreateImageAsync(imageId, imageDto.Description, filePath.FullImagePath, cancellationToken);

                    await scope.Commit(cancellationToken);
                } 
            }
            catch (Exception ex)
            {
                await _fileSystemImageStorage.RemoveImageAsync(filePath.FullImagePath, cancellationToken);
            }
            
            return _mapper.Map<ImageBaseDto>(image);
        }

        public async Task UpdateImageAsync(UpdateImageDto imageDto, CancellationToken cancellationToken)
        {
            var image = await _imageBaseStorage.GetImageAsync(imageDto.Id, cancellationToken);
            if (image is null)
            {
                throw new EntityNotFoundException(nameof(image), imageDto.Id);
            }

            await _imageBaseStorage.UpdateImageAsync(imageDto.Id, imageDto.Description, cancellationToken);

            var imageCopy = await _imageCopyStorage.GetImageAsync(image.Id, cancellationToken);
            if (imageCopy is not null)
            {
                await _imageCopyStorage.UpdateImageCopyAsync(imageCopy.Id, imageDto.Description, cancellationToken);
            }
        }

        public async Task<IEnumerable<ImageBaseViewDto>> GetAllImagesAsync(CancellationToken cancellationToken)
        {
            var images = await _imageBaseStorage.GetAllImagesAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ImageBaseViewDto>>(images);
        }

        public async Task RemoveImageAsync(Guid id, CancellationToken cancellationToken)
        {
            
            var image = await _imageBaseStorage.GetImageAsync(id, cancellationToken);
            if (image is null)
            {
                throw new EntityNotFoundException(nameof(image), id);
            }

            var copyImage = await _imageCopyStorage.GetImageAsync(image.Id, cancellationToken);
            if (copyImage is not null)
            {
                await _imageCopyStorage.RemoveImageAsync(copyImage.Id, cancellationToken);
            }

            await _imageBaseStorage.RemoveImageAsync(id, cancellationToken);

            await _fileSystemImageStorage.RemoveImageAsync(image.FilePath, cancellationToken);
        }
    }
}
