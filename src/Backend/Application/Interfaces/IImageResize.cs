using Application.Configuration;

namespace Application.Interfaces;

public interface IImageResize
{
    public (Stream FullImageStream, Stream ThumbnailStream) Resize(
        Stream originalStream,
        ImageResizeOptions settings);
}