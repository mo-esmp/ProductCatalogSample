using Catalog.Api.V1.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Catalog.Api.V1.Queries
{
    public class ProductGetsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}