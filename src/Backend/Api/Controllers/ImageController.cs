using Api.Extensions.Filters;
using Api.Models.Request;
using Api.Models.Response;
using Application.Models;
using Application.Services.ImageBaseService;
using Application.Services.ImageCopyService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageBaseService _imageBaseService;
        private readonly IImageCopyService _imageCopyService;

        public ImageController(
            IMapper mapper,
            IImageBaseService imageBaseService,
            IImageCopyService imageCopyService)
        {
            _mapper = mapper;
            _imageBaseService = imageBaseService;
            _imageCopyService = imageCopyService;
        }

        /// <summary>
        /// Получение изображений с url.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllImages(
            CancellationToken cancellationToken)
        {
            var image = await _imageBaseService.GetAllImagesAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ImageBaseViewResponse>>(image));
        }

        /// <summary>
        /// Копирование изображения во вторую бд.
        /// </summary>
        [HttpPost("{id}/copy")]
        public async Task<IActionResult> CopyImage(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var copiedImage = await _imageCopyService.CopyImageBaseAsync(id, cancellationToken);
            return Ok(_mapper.Map<ImageCopyResponse>(copiedImage));
        }

        /// <summary>
        /// Создание изображения в перву бд.
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter<CreateImageRequest>))]
        public async Task<IActionResult> CreateImage(
            CreateImageRequest request,
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateImageDto>(request);
            var createdImage = await _imageBaseService.CreateImageAsync(dto, cancellationToken);
            return Ok(_mapper.Map<ImageBaseResponse>(createdImage));
        }

        /// <summary>
        /// Обновление изображения (так же обновится запись во 2 бд).
        /// </summary>
        [HttpPatch]
        [ServiceFilter(typeof(ValidationFilter<UpdateImageRequest>))]
        public async Task<IActionResult> UpdateImage(
            UpdateImageRequest request,
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateImageDto>(request);
            await _imageBaseService.UpdateImageAsync(dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Удаление изображения (так же удалится запись из 2 бд).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await _imageBaseService.RemoveImageAsync(id, cancellationToken);
            return Ok();
        }
    }
}
