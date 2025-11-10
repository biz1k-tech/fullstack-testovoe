using Domain.Exceptions.Base;

namespace Domain.Exceptions
{
    public class ImageAlreadyCopiedException : DomainException
    {
        public ImageAlreadyCopiedException(Guid entityId)
            : base(DomainErrorCode.Conflict, $"Image with id:{entityId} has already been copied!")
        {
        }
    }
}
