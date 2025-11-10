namespace Application.Models;

public record ImageCopyDto
{
    public Guid Id { get; init; }
    public Guid SourceId { get; init; }
    public string SourceDescription { get; init; } = string.Empty;
}