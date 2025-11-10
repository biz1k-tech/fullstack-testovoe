using Application.Models;
using AutoMapper;
using Domain.Entity;
using Shared.ViewModel;

namespace Shared.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ImageBase, ImageBaseDto>();
        CreateMap<ImageBaseDto, ImageViewModel>();
        CreateMap<UpdateImageViewModel, UpdateImageDto>();
    }
}