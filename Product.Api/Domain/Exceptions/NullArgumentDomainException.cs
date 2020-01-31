namespace Product.Api.Domain.Exceptions
{
    public class NullArgumentDomainException : DomainException
    {
        public NullArgumentDomainException(string message)
            : base(message)
        {
        }
    }
}