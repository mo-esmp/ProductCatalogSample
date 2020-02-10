using Microsoft.AspNetCore.Http;
using System;

namespace Catalog.WebClient.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }

        public IFormFile Photo { get; set; }

        public string PhotoUrl { get; set; }
    }
}