namespace Product.Api.Domain.Exceptions
{
    public class InvalidPriceDomainException : DomainException
    {
        public InvalidPriceDomainException(string message) : base(message)
        {
        }
    }
}