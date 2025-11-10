using Application.Configuration;
using Application.Services.ImageBaseService;
using Application.Storages;
using Infrastructure.Storages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mapping;

namespace Shared.Providers
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IImageBaseService, ImageBaseService>();

            services.AddHttpClient<IImageBaseStorage, ImageBaseStorage>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            services.AddAutoMapper(cong=> cong.AddMaps(typeof(MappingProfile)));

            services.Configure<ApiSettingsOptions>(
                configuration.GetSection("ApiSettings"));
            return services;
        }
    }
}
