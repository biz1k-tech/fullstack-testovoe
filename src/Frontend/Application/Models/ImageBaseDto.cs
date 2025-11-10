namespace Application.Models;

public class ImageBaseDto
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public string OriginalImageUrl { get; init; }
    public string ResizeImageUrl { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public bool IsCopied { get; init; }
}