using Application.Configuration;
using Application.Models;

namespace Application.Interfaces
{
    public interface IImageResize
    {
        public (Stream FullImageStream, Stream ThumbnailStream) Resize(
            Stream originalStream,
            ImageResizeOptions settings);
    }
}
