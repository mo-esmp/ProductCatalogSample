using Catalog.Api.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class ProductName : IEquatable<ProductName>
    {
        public ProductName(string value)
        {
            Value = value ?? throw new NullArgumentDomainException("Product name should not be empty.");
        }

        public string Value { get; }

        public bool Equals(ProductName other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((ProductName)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(ProductName value1, ProductName value2)
        {
            return EqualityComparer<ProductName>.Default.Equals(value1, value2);
        }

        public static bool operator !=(ProductName value1, ProductName value2)
        {
            return !(value1 == value2);
        }
    }
}