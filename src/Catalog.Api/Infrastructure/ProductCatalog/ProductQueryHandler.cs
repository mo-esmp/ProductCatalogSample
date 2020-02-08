using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Infrastructure.Data;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure.ProductCatalog
{
    public class ProductQueryHandler :
        IRequestHandler<V1.Queries.ProductGetQuery, V1.Dtos.ProductDto>,
        IRequestHandler<V1.Queries.ProductSearchQuery, IEnumerable<V1.Dtos.ProductDto>>
    {
        private readonly ProductDbContext _context;

        public ProductQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<V1.Dtos.ProductDto> Handle(V1.Queries.ProductGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => new V1.Dtos.ProductDto
                {
                    Id = p.Id,
                    Code = p.Code.Value,
                    Name = p.Name.Value,
                    Price = p.Price.Amount,
                    CurrencyCode = p.Price.Currency.CurrencyCode
                })
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<V1.Dtos.ProductDto>> Handle(V1.Queries.ProductSearchQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<Product>(sc => true);

            if (request.Code != null)
                predicate = predicate.And(p => p.Code.Value.Contains(request.Code));

            if (request.Name != null)
                predicate = predicate.And(p => p.Name.Value.Contains(request.Name));

            return await _context.Products
                .AsNoTracking()
                .Where(predicate)
                .Select(p => new V1.Dtos.ProductDto
                {
                    Id = p.Id,
                    Code = p.Code.Value,
                    Name = p.Name.Value,
                    Price = p.Price.Amount,
                    CurrencyCode = p.Price.Currency.CurrencyCode
                })
                .ToListAsync(cancellationToken);
        }
    }
}