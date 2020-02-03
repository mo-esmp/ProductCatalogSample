using Catalog.Api.Domain.Shared;
using System;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class ProductCodeChangedEvent : Event
    {
        public ProductCodeChangedEvent(Guid id, string code)
        {
            AggregateId = id;
            Code = code;
        }

        public string Code { get; }
    }
}