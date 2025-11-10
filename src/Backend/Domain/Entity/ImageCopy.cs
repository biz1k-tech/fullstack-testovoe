using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class ImageCopy
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        [MaxLength(50)]
        public string SourceDescription { get; set; } = string.Empty;
        public byte[] Blob { get; set; } = Array.Empty<byte>();
    }
}
