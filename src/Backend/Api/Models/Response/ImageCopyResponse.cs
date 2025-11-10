namespace Api.Models.Response
{
    public class ImageCopyResponse
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public string SourceDescription { get; set; }
    }
}
