namespace Catalog.Api.Domain.Exceptions
{
    public class DuplicateDomainException : DomainException
    {
        public DuplicateDomainException(string message) : base(message)
        {
        }
    }
}