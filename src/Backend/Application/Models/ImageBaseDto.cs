namespace Application.Models;

/// <summary>
///     DTO для работы на сервере
/// </summary>
public record ImageBaseDto
{
    public Guid Id { get; init; }
    public string FilePath { get; init; }
    public string Description { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public bool IsCopied { get; init; }
}