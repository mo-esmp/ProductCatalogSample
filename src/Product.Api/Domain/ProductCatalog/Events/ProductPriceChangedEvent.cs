using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class ProductPriceChangedEvent : Event
    {
        public ProductPriceChangedEvent(Guid id, Money price)
        {
            AggregateId = id;
            Price = price;
        }

        public Money Price { get; }
    }
}