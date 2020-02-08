using Catalog.Api.V1.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Catalog.Api.V1.Queries
{
    public class ProductSearchQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}