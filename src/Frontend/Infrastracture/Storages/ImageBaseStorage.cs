using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Application.Storages;
using Domain.Entity;

namespace Infrastructure.Storages;

public class ImageBaseStorage : IImageBaseStorage
{
    private readonly HttpClient _httpClient;

    public ImageBaseStorage(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CopyImage(Guid id)
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/images/{id}/copy", null);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> CreateImage(string description, Stream fileStream, string fileName, string contentType)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(description), "Description");

        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        content.Add(fileContent, "File", fileName);

        try
        {
            var response = await _httpClient.PostAsync("api/images", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<ImageBase>>? GetAllImages()
    {
        IEnumerable<ImageBase> images = null;
        try
        {
            var response = await _httpClient.GetAsync("api/images");
            images = await response.Content.ReadFromJsonAsync<List<ImageBase>>();

            return images;
        }
        catch (Exception ex)
        {
        }

        return images;
    }

    public async Task<bool> RemoveImage(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/images/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> UpdateImage(Guid id, string description)
    {
        try
        {
            using StringContent content = new(
                JsonSerializer.Serialize(new
                {
                    Id = id,
                    Description = description
                }),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PatchAsync("api/images", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}