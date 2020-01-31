using Product.Api.Domain.Shared;
using System;

namespace Product.Api.Domain.ProductCatalog
{
    public class Photo : Entity<Guid>
    {
        // Properties to handle the persistence
        public Guid PictureId
        {
            get => Id.Value;
            set { }
        }

        protected Photo()
        {
        }

        // Entity state
        public ClassifiedAdId ParentId { get; private set; }

        public PictureSize Size { get; private set; }

        public string Location { get; private set; }

        public int Order { get; private set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.Events.PictureAddedToAClassifiedAd e:
                    ParentId = new ClassifiedAdId(e.ClassifiedAdId);
                    Id = new PictureId(e.PictureId);
                    Location = e.Url;
                    Size = new PictureSize { Height = e.Height, Width = e.Width };
                    Order = e.Order;
                    break;

                case Events.Events.ClassifiedAdPictureResized e:
                    Size = new PictureSize { Height = e.Height, Width = e.Width };
                    break;
            }
        }

        public void Resize(PictureSize newSize)
            => Apply(new Events.Events.ClassifiedAdPictureResized
            {
                PictureId = Id.Value,
                ClassifiedAdId = ParentId.Value,
                Height = newSize.Width,
                Width = newSize.Width
            });

        public Photo(Action<object> applier) : base(applier)
        {
        }
    }

    public class PictureId : Value<PictureId>
    {
        public PictureId(Guid value) => Value = value;

        public Guid Value { get; }

        protected PictureId()
        {
        }
    }
}