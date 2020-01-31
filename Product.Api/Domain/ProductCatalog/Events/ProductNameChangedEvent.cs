using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class ProductNameChangedEvent : Event
    {
        public ProductNameChangedEvent(Guid id, string name)
        {
            AggregateId = id;
            Name = name;
        }

        public string Name { get; }
    }
}