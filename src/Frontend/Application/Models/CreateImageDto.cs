using Microsoft.AspNetCore.Components.Forms;

namespace Application.Models
{
    public class CreateImageDto
    {
        public string Description { get; set; }
        public Stream FileStream { get; set; } 
        public string FileName { get; set; }   
        public string ContentType { get; set; }
    }
}
