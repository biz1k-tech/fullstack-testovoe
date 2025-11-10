using Application.Models;
using Application.Services.ImageBaseService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Const;
using Shared.ViewModel;

namespace Web.Client.Pages.ImageUpload;

public partial class ImageUpload : ComponentBase
{
    [Inject] public IImageBaseService ImageBaseService { get; set; }

    private bool isUploading;
    private CreateImageViewModel ViewModel { get; set; } = new();

    protected override void OnInitialized()
    {
    }

    public void LoadFile(InputFileChangeEventArgs e)
    {
        ViewModel.ImageBrowserFile = e.File;
    }

    public async Task CreateImageAsync()
    {
        using (var stream =
               ViewModel.ImageBrowserFile.OpenReadStream(ValidationConst.MaxFileSizeMb * 1024 * 1024))
        {
            var imageDto = new CreateImageDto
            {
                Description = ViewModel.Description,
                FileStream = stream,
                FileName = ViewModel.ImageBrowserFile.Name,
                ContentType = ViewModel.ImageBrowserFile.ContentType
            };

            var response = await ImageBaseService.CreateImageAsync(imageDto);

            if (response)
            {
                isUploading = true;
            }

            ViewModel = new CreateImageViewModel();
        }
    }
}