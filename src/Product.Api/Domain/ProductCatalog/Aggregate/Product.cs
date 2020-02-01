using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class Product : AggregateRoot<Guid>
    {
        public Product(string code, string name, Money price)
        {
            Apply(new ProductCreatedEvent(Guid.NewGuid(), code, name, price));
        }

        public ProductCode Code { get; private set; }

        public ProductName Name { get; private set; }

        public ProductPrice Price { get; private set; }

        public Photo Photo { get; private set; }

        public DateTime? LastUpdateDate { get; private set; }

        public void UpdateCode(string code) => Apply(new ProductCodeChangedEvent(Id, code));

        public void UpdateName(string name) => Apply(new ProductNameChangedEvent(Id, name));

        public void UpdatePrice(Money price) => Apply(new ProductPriceChangedEvent(Id, price));

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ProductCreatedEvent e:
                    ApplyProductCreatedEvent(e);
                    break;

                case ProductCodeChangedEvent e:
                    ApplyProductCodeChangedEvent(e);
                    break;

                case ProductNameChangedEvent e:
                    ApplyProductNameChangedEvent(e);
                    break;

                case ProductPriceChangedEvent e:
                    ApplyProductPriceChangedEvent(e);
                    break;
            }
        }

        private void ApplyProductCreatedEvent(ProductCreatedEvent e)
        {
            Id = e.AggregateId;
            Code = new ProductCode(e.Code);
            Name = new ProductName(e.Name);
            Price = ProductPrice.FromMoney(e.Price);
        }

        private void ApplyProductCodeChangedEvent(ProductCodeChangedEvent e)
        {
            Code = new ProductCode(e.Code);
            LastUpdateDate = DateTime.UtcNow;
        }

        private void ApplyProductNameChangedEvent(ProductNameChangedEvent e)
        {
            Name = new ProductName(e.Name);
            LastUpdateDate = DateTime.UtcNow;
        }

        private void ApplyProductPriceChangedEvent(ProductPriceChangedEvent e)
        {
            Price = ProductPrice.FromMoney(e.Price);
            LastUpdateDate = DateTime.UtcNow;
        }
    }
}