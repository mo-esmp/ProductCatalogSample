using System;
using System.Collections.Generic;

namespace Product.Api.Domain.Shared
{
    public abstract class Entity<T> : IEquatable<Entity<T>>
    {
        protected Entity()
        {
        }

        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<T>);
        }

        public bool Equals(Entity<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Entity<T> entity1, Entity<T> entity2)
        {
            return EqualityComparer<Entity<T>>.Default.Equals(entity1, entity2);
        }

        public static bool operator !=(Entity<T> entity1, Entity<T> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}