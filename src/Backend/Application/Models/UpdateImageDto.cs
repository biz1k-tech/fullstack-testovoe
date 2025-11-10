namespace Application.Models;

public record UpdateImageDto
{
    public Guid Id { get; init; }
    public string Description { get; init; }
}