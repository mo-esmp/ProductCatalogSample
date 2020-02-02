using Product.Api.Domain.Shared;
using Product.Api.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Product.Api.V1.ViewModels
{
    public class ProductAddViewModel
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
    }
}