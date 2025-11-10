using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;

namespace Application.Services.ImageCopyService;

public class ImageCopyService : IImageCopyService
{
    private readonly IFileSystemImageStorage _fileSystemImageStorage;
    private readonly IImageBaseStorage _imageBaseStorage;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkBase2 _unitOfWork;

    public ImageCopyService(
        IImageBaseStorage imageBaseStorage,
        IImageCopyStorage imageCopyStorage,
        IUnitOfWorkBase2 unitOfWork,
        IFileSystemImageStorage fileSystemImageStorage,
        IMapper mapper)
    {
        _imageBaseStorage = imageBaseStorage;
        _unitOfWork = unitOfWork;
        _fileSystemImageStorage = fileSystemImageStorage;
        _mapper = mapper;
    }

    public async Task<ImageCopyDto> CopyImageBaseAsync(Guid id, CancellationToken cancellationToken)
    {
        var image = await _imageBaseStorage.GetImageAsync(id, cancellationToken);
        if (image is null)
        {
            throw new EntityNotFoundException(nameof(image), id);
        }

        if (image.IsCopied)
        {
            throw new ImageAlreadyCopiedException(id);
        }

        var isImageExists = await _fileSystemImageStorage.CheckFileExistsAsync(image.FilePath,
            cancellationToken);
        if (!isImageExists)
        {
            throw new ImageFileNotFoundException(image.FilePath);
        }

        var imageBytes = await _fileSystemImageStorage.ReadImageAsBytesAsync(
            image.FilePath, cancellationToken);

        ImageCopy copiedImage = null;
        try
        {
            await using (var scope = await _unitOfWork.StartScope(cancellationToken))
            {
                var scopedImageCopyStorage = scope.GetStorage<IImageCopyStorage>();
                copiedImage =
                    await scopedImageCopyStorage.CopyImageBaseAsync(image.Id, image.Description, imageBytes,
                        cancellationToken);

                await scope.Commit(cancellationToken);
            }

            await _imageBaseStorage.SetImageCopiedFlagAsync(image.Id, true, cancellationToken);
        }
        catch (Exception)
        {
            if (copiedImage is not null)
            {
                await _imageBaseStorage.SetImageCopiedFlagAsync(image.Id, false, cancellationToken);
            }
        }

        return _mapper.Map<ImageCopyDto>(copiedImage);
    }
}