using Catalog.Api.V1.Dtos;
using MediatR;
using System;

namespace Catalog.Api.V1.Queries
{
    public class ProductGetQuery : IRequest<ProductDto>
    {
        public Guid ProductId { get; set; }
    }
}