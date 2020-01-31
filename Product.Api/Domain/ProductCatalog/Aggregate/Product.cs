using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class Product : AggregateRoot<Guid>
    {
        public Product(string code, string name, Price price)
        {
            Apply(new ProductCreatedEvent(Guid.NewGuid(), code, name, price));
        }

        public ProductCode Code { get; private set; }

        public ProductName Name { get; private set; }

        public Price Price { get; private set; }

        public Photo Photo { get; private set; }

        public DateTime? LastUpdateDate { get; private set; }

        public void UpdateCode(string code) => Apply(new ProductCodeChangedEvent(Id, code));

        public void UpdateText(string name) => Apply(new ProductNameChangedEvent(Id = Id, Name = name));

        public void UpdatePrice(Price price) =>
            Apply(new Events.Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrencyCode = price.Currency.CurrencyCode
            });

        public void AddPicture(Uri pictureUri, PictureSize size)
        {
            Apply(new Events.Events.PictureAddedToAClassifiedAd
            {
                PictureId = new Guid(),
                ClassifiedAdId = Id,
                Url = pictureUri.ToString(),
                Height = size.Height,
                Width = size.Width,
                Order = NewPictureOrder()
            });

            int NewPictureOrder() =>
                Pictures.Any() ? Pictures.Max(x => x.Order) + 1 : 0;
        }

        public void ResizePicture(PictureId pictureId, PictureSize newSize)
        {
            var picture = FindPicture(pictureId);
            if (picture == null)
                throw new InvalidOperationException(
                    "Cannot resize a picture that I don't have");

            picture.Resize(newSize);
        }

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
            }
        }

        private void ApplyProductCreatedEvent(ProductCreatedEvent e)
        {
            Id = e.AggregateId;
            Code = new ProductCode(e.Code);
            Name = new ProductName(e.Name);
            Price = e.Price;
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
    }
}