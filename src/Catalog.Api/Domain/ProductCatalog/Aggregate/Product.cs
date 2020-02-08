using Catalog.Api.Domain.Shared;
using System;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class Product : AggregateRoot<Guid>
    {
        protected Product()
        {
        }

        public Product(Guid id, string code, string name, Money price, string photoName)
        {
            Apply(new ProductCreatedEvent(id, code, name, price, photoName));
        }

        public ProductCode Code { get; private set; }

        public ProductName Name { get; private set; }

        public ProductPrice Price { get; private set; }

        public ProductStatus Status { get; private set; }

        public string PhotoName { get; private set; }

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
            if (e.AggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(Id));

            Id = e.AggregateId;
            Code = new ProductCode(e.Code);
            Name = new ProductName(e.Name);
            Price = ProductPrice.FromMoney(e.Price);
            PhotoName = e.PhotoName;
            SetStatus();
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
            SetStatus();
            LastUpdateDate = DateTime.UtcNow;
        }

        private void SetStatus()
        {
            Status = Price.Amount <= 999 ? ProductStatus.Active : ProductStatus.Pending;
        }
    }

    public enum ProductStatus
    {
        Active = 1,
        Pending = 2
    }
}