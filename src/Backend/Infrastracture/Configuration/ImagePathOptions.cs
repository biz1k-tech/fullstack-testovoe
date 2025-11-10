namespace Infrastructure.Configuration;

public class ImagePathOptions
{
    public const string SectionName = "Images";

    public string Path { get; set; }
    public string RequestPath { get; set; }
}