using Microsoft.AspNetCore.Components.Forms;

namespace Shared.ViewModel;

public class CreateImageViewModel
{
    public IBrowserFile ImageBrowserFile { get; set; }
    public string Description { get; set; }
}