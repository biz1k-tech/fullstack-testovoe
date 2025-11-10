using AutoMapper;

namespace Api.Mapping
{
    internal class FormFileToStreamConverter : IValueConverter<IFormFile, Stream>
    {
        public Stream Convert(IFormFile source, ResolutionContext context)
        {
            if (source == null)
                return Stream.Null;

            var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);

            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
