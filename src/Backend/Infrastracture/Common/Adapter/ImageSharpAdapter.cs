using Application.Configuration;
using Application.Interfaces;
using Application.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Common.Adapter
{
    public class ImageSharpAdapter : IImageResize
    {
        public (Stream FullImageStream, Stream ThumbnailStream) Resize(Stream originalStream, ImageResizeOptions settings)
        {
            originalStream.Seek(0, SeekOrigin.Begin);

            using var image = Image.Load<Rgba32>(originalStream);

            var fullImage = image.Clone();
            var thumbnailImage = image.Clone();

            MemoryStream fullStream = null;
            MemoryStream thumbStream = null;

            try
            {
                fullStream = new MemoryStream();

                fullImage.Save(fullStream, new JpegEncoder { Quality = settings.JpegQuality });
                fullStream.Seek(0, SeekOrigin.Begin); 

                thumbnailImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(settings.ThumbnailWidth, settings.ThumbnailHeight),
                    Mode = ResizeMode.Max 
                }));

                thumbStream = new MemoryStream();

                thumbnailImage.Save(thumbStream, new JpegEncoder { Quality = settings.JpegQuality });
                thumbStream.Seek(0, SeekOrigin.Begin); 

                return (fullStream, thumbStream);
            }
            finally
            {
                fullImage?.Dispose();
                thumbnailImage?.Dispose();
            }
        }
    }
}
