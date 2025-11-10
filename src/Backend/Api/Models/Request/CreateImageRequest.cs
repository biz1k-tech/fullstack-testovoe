namespace Api.Models.Request;

public class CreateImageRequest
{
    public string Description { get; set; }
    public IFormFile File { get; set; }
}