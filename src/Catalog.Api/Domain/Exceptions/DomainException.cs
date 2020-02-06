using System;

namespace Catalog.Api.Domain.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {
        }
    }
}