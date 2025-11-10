using AutoMapper;

namespace Api.Mapping;

internal class ExtensionFromContentTypeConverter : IValueConverter<string, string>
{
    public string Convert(string contentType, ResolutionContext context) =>
        contentType switch
        {
            "image/jpeg" => ".jpg",
            "image/jpg" => ".jpg",
            "image/png" => ".png",
            "image/webp" => ".webp",
            _ => ".bin"
        };
}