namespace Domain.Entity
{
    public class ImageBase
    {
        public Guid Id { get; set; }
        public string Description { get; set; } 
        public string OriginalImageUrl { get; set; }
        public string ResizeImageUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsCopied { get; set; }
    }
}
