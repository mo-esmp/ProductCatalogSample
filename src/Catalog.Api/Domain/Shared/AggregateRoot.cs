using Catalog.Api.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Api.Domain.Shared
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private readonly List<object> _events;

        protected AggregateRoot() => _events = new List<object>();

        public int Version { get; } = 1;

        public void ValidateVersion(int expectedVersion)
        {
            if (Version != expectedVersion)
                throw new ConcurrencyDomainException(
                    $"Invalid version specified : expected version is {Version} but original version is {expectedVersion}.");
        }

        public IEnumerable<object> GetChanges() => _events.AsEnumerable();

        public void ClearChanges() => _events.Clear();

        protected abstract void When(object @event);

        protected void Apply(object @event, int originalVersion = 1)
        {
            ValidateVersion(originalVersion);
            When(@event);
            _events.Add(@event);
        }
    }
}