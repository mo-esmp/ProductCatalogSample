using MediatR;
using System;

namespace Catalog.Api.Domain.Shared
{
    public abstract class Event : INotification
    {
        public Guid AggregateId { get; protected set; }

        public Guid EventId => Guid.NewGuid();

        public long AggregateVersion { get; private set; }

        public DateTime OccurredOn => DateTime.UtcNow;

        public void BuildVersion(long aggregateVersion)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}