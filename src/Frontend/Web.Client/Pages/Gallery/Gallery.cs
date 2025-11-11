using Application.Models;
using Application.Services.ImageBaseService;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Shared.ViewModel;
using Web.Client.Components;

namespace Web.Client.Pages.Gallery;

public partial class Gallery : ComponentBase
{
    private ConfirmModalRemove _confirmModalRemove = default!;
    private Guid _selectedId;
    private UpdateImageViewModel _updateImageViewModel = new();
    private List<ImageViewModel> imagesViewModels;
    private bool isLoad;

    [Inject] private IMapper _mapper { get; set; }
    [Inject] public IImageBaseService ImageBaseService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var imagesDto = await ImageBaseService.GetAllImagesAsync();
        imagesViewModels = _mapper.Map<List<ImageViewModel>>(imagesDto);

        isLoad = true;
    }

    public async Task UpdateImageAsync(Guid id)
    {
        _updateImageViewModel.Id = id;
        var dto = _mapper.Map<UpdateImageDto>(_updateImageViewModel);

        await ImageBaseService.UpdateImage(dto);

        await RefreshData();
    }

    public async Task RemoveImageAsync(Guid id)
    {
        await ImageBaseService.RemoveImage(id);

        await RefreshData();
    }

    public async Task CopyImageImageAsync(Guid id)
    {
        await ImageBaseService.CopyImage(id);

        await RefreshData();
    }

    private async Task ShowConfirmModal(Guid id)
    {
        _selectedId = id;
        await _confirmModalRemove.ShowAsync();
    }

    private async Task ConfirmRemoveAsync()
    {
        await RemoveImageAsync(_selectedId);
        await _confirmModalRemove.HideAsync();
    }

    private async Task RefreshData()
    {
        var dto = await ImageBaseService.GetAllImagesAsync();

        _updateImageViewModel = new UpdateImageViewModel();

        imagesViewModels = _mapper.Map<List<ImageViewModel>>(dto);
        StateHasChanged();
    }
}