﻿using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Domain.Shared;
using Catalog.Api.Infrastructure;
using System;
using System.Collections.Generic;

namespace IntegrationTests
{
    internal class InitialProductList
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "12345", "Sample Product 1",
                Money.FromDecimal(10, CurrencyCode.Euro, new FixedCurrencyLookup())),

            new Product(Guid.NewGuid(), "67890", "Sample Product 2",
                Money.FromDecimal(20, CurrencyCode.Euro, new FixedCurrencyLookup()))
        };

        public static IEnumerable<Product> Products => _products;
    }
}