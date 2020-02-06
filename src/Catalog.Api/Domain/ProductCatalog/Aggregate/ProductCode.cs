using Catalog.Api.Domain.Exceptions;
using Catalog.Api.Resources;
using System;
using System.Collections.Generic;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class ProductCode : IEquatable<ProductCode>
    {
        public ProductCode(string value)
        {
            Value = value ?? throw new NullArgumentDomainException(
                        string.Format(ErrorMessagesResource.NullArgumentException, DisplayNamesResource.ProductCode));
        }

        public string Value { get; }

        public bool Equals(ProductCode other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductCode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(ProductCode value1, ProductCode value2)
        {
            return EqualityComparer<ProductCode>.Default.Equals(value1, value2);
        }

        public static bool operator !=(ProductCode value1, ProductCode value2)
        {
            return !(value1 == value2);
        }
    }
}