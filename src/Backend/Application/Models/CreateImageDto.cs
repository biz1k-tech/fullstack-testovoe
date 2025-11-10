namespace Application.Models;

public record CreateImageDto
{
    public string Description { get; init; }
    public Stream FileStream { get; init; }
    public string Extension { get; init; }
}