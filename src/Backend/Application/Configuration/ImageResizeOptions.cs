namespace Application.Configuration;

public class ImageResizeOptions
{
    public const string SectionName = "ImageResize";

    public int ThumbnailWidth { get; set; } = 200;
    public int ThumbnailHeight { get; set; } = 200;
    public int JpegQuality { get; set; } = 75;
}