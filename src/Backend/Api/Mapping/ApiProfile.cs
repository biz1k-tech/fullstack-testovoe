using AutoMapper;
using Application.Models;
using Api.Models.Request;
using Api.Models.Response;

namespace Api.Mapping
{
    internal class ApiProfile:Profile
    {
        public ApiProfile()
        {
            CreateMap<CreateImageRequest, CreateImageDto>()
                .ForMember(
                    dest => dest.FileStream,
                    opt => opt.ConvertUsing(
                        new FormFileToStreamConverter(), src => src.File)
                )
                .ForMember(
                    dest => dest.Extension, opt => opt.ConvertUsing(
                    new ExtensionFromContentTypeConverter(), src => src.File.ContentType));
            CreateMap<ImageBaseDto, ImageBaseResponse>();
            CreateMap<CreateImageDto, ImageBaseResponse>();
            CreateMap<UpdateImageRequest, UpdateImageDto>();
            CreateMap<ImageCopyDto, ImageBaseResponse>();
            CreateMap<ImageCopyDto, ImageCopyResponse>();
            CreateMap<ImageBaseViewDto, ImageBaseViewResponse>();
        }
    }
}
