using System;

namespace Product.Api.Domain.Exceptions
{
    public class ConcurrencyDomainException : Exception
    {
        public ConcurrencyDomainException(string message) : base(message)
        {
        }
    }
}