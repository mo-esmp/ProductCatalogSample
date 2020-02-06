using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text.Json.Serialization;

namespace Catalog.Api.V1.Commands
{
    public class ProductEditCommand : ProductAddCommand
    {
        [BindNever, JsonIgnore]
        public Guid Id { get; set; }
    }
}