﻿using Catalog.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure.ProductCatalog
{
    public class ProductQueryHandler :
        IRequestHandler<V1.Queries.ProductGetQuery, V1.Dtos.ProductDto>,
    {
        private readonly ProductDbContext _context;

        public ProductQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public Task<V1.Dtos.ProductDto> Handle(V1.Queries.ProductGetQuery request, CancellationToken cancellationToken)
        {
            return _context.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => new V1.Dtos.ProductDto
                {
                    Id = p.Id,
                    Code = p.Code.Value,
                    Name = p.Name.Value,
                    Price = p.Price.Amount,
                    Currency = p.Price.Currency.ToString()
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}