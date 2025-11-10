namespace Domain.Exceptions.Base;

public abstract class DomainException(DomainErrorCode domainErrorCode, string message) : Exception(message)
{
    public DomainErrorCode DomainErrorCode { get; } = domainErrorCode;
}