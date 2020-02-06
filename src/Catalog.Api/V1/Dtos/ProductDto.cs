﻿using System;

namespace Catalog.Api.V1.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }
    }
}