namespace Application.Models
{
    /// <summary>
    /// DTO для отправки на фронт
    /// </summary>
    public record ImageBaseViewDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public string OriginalImageUrl { get; init; }
        public string ResizeImageUrl { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public bool IsCopied { get; init; }
    }
}
