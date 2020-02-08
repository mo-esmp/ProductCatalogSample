using Catalog.Api.Domain.Shared;
using Catalog.Api.Resources;
using Catalog.Api.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Api.V1.Commands
{
    public class ProductAddCommand : IRequest<Guid>
    {
        [Display(ResourceType = typeof(DisplayNamesResource), Name = "ProductCode")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesResource), ErrorMessageResourceName = "RequiredError")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(DisplayNamesResource), Name = "ProductName")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesResource), ErrorMessageResourceName = "RequiredError")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DisplayNamesResource), Name = "ProductPrice")]
        [Range(typeof(decimal), "1", "9999999", ErrorMessageResourceType = typeof(ErrorMessagesResource), ErrorMessageResourceName = "InvalidPriceError")]
        public decimal Price { get; set; }

        [Display(ResourceType = typeof(DisplayNamesResource), Name = "Currency")]
        [Range(1, 2, ErrorMessageResourceType = typeof(ErrorMessagesResource), ErrorMessageResourceName = "InvalidCurrencyError")]
        public CurrencyCode CurrencyCode { get; set; }

        public IFormFile Photo { get; set; }

        [BindNever, JsonIgnore, SwaggerExclude]
        public string PhotoName { get; set; }
    }
}