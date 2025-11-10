namespace Shared.ViewModel;

public class ImageViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string OriginalImageUrl { get; set; }
    public string ResizeImageUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsCopied { get; set; }
}