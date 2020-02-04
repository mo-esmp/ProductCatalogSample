using System;

namespace Catalog.Api.Domain.Exceptions
{
    public class CurrencyMismatchDomainException : Exception
    {
        public CurrencyMismatchDomainException(string message) : base(message)
        {
        }
    }
}