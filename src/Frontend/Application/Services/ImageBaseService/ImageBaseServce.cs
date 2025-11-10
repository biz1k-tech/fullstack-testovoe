using Application.Configuration;
using Application.Models;
using Application.Storages;
using AutoMapper;
using Domain.Entity;
using Microsoft.Extensions.Options;

namespace Application.Services.ImageBaseService;

public class ImageBaseService : IImageBaseService
{
    private readonly IImageBaseStorage _imageBaseStorage;
    private readonly IMapper _mapper;
    private readonly IOptions<ApiSettingsOptions> _options;

    public ImageBaseService(
        IImageBaseStorage imageBaseStorage,
        IOptions<ApiSettingsOptions> options,
        IMapper mapper)
    {
        _imageBaseStorage = imageBaseStorage;
        _options = options;
        _mapper = mapper;
    }

    public async Task<bool> CopyImage(Guid id)
    {
        return await _imageBaseStorage.CopyImage(id);
    }

    public async Task<bool> CreateImageAsync(CreateImageDto imageDto)
    {
        return await _imageBaseStorage.CreateImage(imageDto.Description, imageDto.FileStream,
            imageDto.FileName, imageDto.ContentType);
    }

    public async Task<IEnumerable<ImageBaseDto>?> GetAllImagesAsync()
    {
        var images = await _imageBaseStorage.GetAllImages();
        if (images == null || !images.Any())
        {
            return Enumerable.Empty<ImageBaseDto>();
        }
        foreach (var image in images)
        {
            image.OriginalImageUrl = string.Concat(_options.Value.BaseUrl, image.OriginalImageUrl);
            image.ResizeImageUrl = string.Concat(_options.Value.BaseUrl, image.ResizeImageUrl);
        }

        return _mapper.Map<IEnumerable<ImageBaseDto>>(images.OrderBy(x => x.CreatedAt)
                                                      ?? Enumerable.Empty<ImageBase>());
    }

    public async Task<bool> RemoveImage(Guid id)
    {
        return await _imageBaseStorage.RemoveImage(id);
    }

    public async Task<bool> UpdateImage(UpdateImageDto imageDto)
    {
        return await _imageBaseStorage.UpdateImage(imageDto.id, imageDto.Description);
    }
}