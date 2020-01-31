using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(Guid id, string code, string name, Price price)
        {
            AggregateId = id;
            Code = code;
            Name = name;
            Price = price;
        }

        public string Code { get; }

        public string Name { get; }

        public Price Price { get; }
    }
}