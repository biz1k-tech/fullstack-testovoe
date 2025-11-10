using Application.Models.Enum;

namespace Application.Mapping
{
    public static class ResizeUrlConverter
    {
        public static string GetUrl(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);
            var lastDashIndex = fileName.LastIndexOf('-');
            var idPart = lastDashIndex > 0 ? fileName[..lastDashIndex] : fileName;
            return $"/uploads/{idPart}-{(ImageSuffix.Resize).ToString().ToLowerInvariant()}{ext}";
        }
    }
}
