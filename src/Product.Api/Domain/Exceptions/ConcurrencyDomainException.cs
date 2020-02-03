using System;

namespace Catalog.Api.Domain.Exceptions
{
    public class ConcurrencyDomainException : Exception
    {
        public ConcurrencyDomainException(string message) : base(message)
        {
        }
    }
}