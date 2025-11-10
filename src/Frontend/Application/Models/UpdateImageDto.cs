namespace Application.Models;

public record UpdateImageDto
{
    public Guid id { get; init; }
    public string Description { get; init; }
}