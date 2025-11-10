using Application.Models;
using Application.Models.Enum;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<CreateImageDto, ImageBase>().ReverseMap();
        CreateMap<ImageBaseDto, ImageBase>().ReverseMap();
        CreateMap<ImageCopyDto, ImageCopy>().ReverseMap();
        CreateMap<ImageBaseViewDto, ImageBase>().ReverseMap()
            .ForMember(dest => dest.OriginalImageUrl,
                opt => opt.MapFrom(src => GetOriginalUrl(src.FilePath)))
            .ForMember(dest => dest.ResizeImageUrl,
                opt => opt.MapFrom(src => GetUrlWithChangedSuffix(src.FilePath, ImageSuffix.Resize)));
    }

    private string GetOriginalUrl(string filePath)
    {
        return $"/uploads/{Path.GetFileName(filePath)}";
    }

    private string GetUrlWithChangedSuffix(string filePath, ImageSuffix newSuffix)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var ext = Path.GetExtension(filePath);
        var lastDashIndex = fileName.LastIndexOf('-');
        var idPart = lastDashIndex > 0 ? fileName[..lastDashIndex] : fileName;
        return $"/uploads/{idPart}-{newSuffix.ToString().ToLowerInvariant()}{ext}";
    }
}