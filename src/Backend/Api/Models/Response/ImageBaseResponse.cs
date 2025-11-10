namespace Api.Models.Response
{
    public class ImageBaseResponse
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsCopied { get; set; }
    }
}
