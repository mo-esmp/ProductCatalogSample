using Catalog.Api.Domain.Shared;
using System;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(Guid id, string code, string name, Money price, string photoName)
        {
            AggregateId = id;
            Code = code;
            Name = name;
            Price = price;
            PhotoName = photoName;
        }

        public string Code { get; }

        public string Name { get; }

        public Money Price { get; }

        public string PhotoName { get; }
    }
}