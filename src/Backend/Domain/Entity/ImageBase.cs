using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;

public class ImageBase
{
    [Key] public Guid Id { get; set; }

    public string FilePath { get; set; } = string.Empty;

    [MaxLength(50)] public string Description { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
    public bool IsCopied { get; set; }
}