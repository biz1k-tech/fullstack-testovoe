using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public class ImageFileNotFoundException : DomainException
{
    public ImageFileNotFoundException(string filePath)
        : base(DomainErrorCode.NotFound, $"File does not exist on the file system:{filePath}")
    {
    }
}