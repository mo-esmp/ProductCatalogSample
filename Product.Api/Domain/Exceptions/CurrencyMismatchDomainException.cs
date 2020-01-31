using System;

namespace Product.Api.Domain.Exceptions
{
    public class CurrencyMismatchDomainException : Exception
    {
        public CurrencyMismatchDomainException(string message) : base(message)
        {
        }
    }
}