using Api.Common.Validation;
using Api.Extensions.Filters;
using Api.Mapping;
using Application.Interfaces;
using Application.Mapping;
using Application.Services.ImageBaseService;
using Application.Services.ImageCopyService;
using FluentValidation;
using Infrastructure.Common.Storages;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Application.Models;
using Infrastructure.Common.Adapter;
using Infrastructure.Configuration;
using Application.Configuration;

namespace Api.Extensions.Providers
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services,
            IConfiguration configuration)
        {

            services
                .AddScoped<IImageBaseService, ImageBaseService>()
                .AddScoped<IImageBaseStorage, ImageBaseStorage>()
                .AddScoped<IImageCopyService, ImageCopyService>()
                .AddScoped<IImageCopyStorage, ImageCopyStorage>()
                .AddScoped<IFileSystemImageStorage, FileSystemImageStorage>()
                .AddScoped<IUnitOfWorkBase1, UnifOfWorkBase1>()
                .AddScoped<IUnitOfWorkBase2, UnifOfWorkBase2>()
                .AddScoped<IImageResize, ImageSharpAdapter>()
                .AddScoped(typeof(ValidationFilter<>)); 

            services.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetAssembly(typeof(ApiProfile)));
                config.AddMaps(Assembly.GetAssembly(typeof(ApplicationProfile)));
            });
            services
                .AddDbContextPool<ContextBase1>(options => options
                    .UseNpgsql(configuration["ConnectionStrings:Base1Connection"]));
            services
                .AddDbContextPool<ContextBase2>(options => options
                    .UseNpgsql(configuration["ConnectionStrings:Base2Connection"]));
           services.Configure<ImageResizeOptions>(
                configuration.GetSection(ImageResizeOptions.SectionName));

           services.Configure<ImagePathOptions>(
               configuration.GetSection(ImagePathOptions.SectionName));

            services.AddValidatorsFromAssemblyContaining<CreateImageRequestValidator>();

            return services;
        }
    }
}
