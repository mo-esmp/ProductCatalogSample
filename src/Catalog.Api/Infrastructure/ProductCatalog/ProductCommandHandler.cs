using Catalog.Api.Domain.Exceptions;
using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Domain.Shared;
using Catalog.Api.Resources;
using Catalog.Api.V1.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure.ProductCatalog
{
    public class ProductCommandHandler : IRequestHandler<ProductAddCommand>
    {
        private readonly ICurrencyLookup _currencyLookup;
        private readonly IProductRepository _repository;

        public ProductCommandHandler(ICurrencyLookup currencyLookup, IProductRepository repository)
        {
            _currencyLookup = currencyLookup;
            _repository = repository;
        }

        public async Task<Unit> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.CheckProductExistByCodeAsync(request.Code))
                throw new DuplicateDomainException(string.Format(ErrorMessagesResource.DuplicateError, DisplayNamesResource.ProductCode));

            var price = Money.FromDecimal(request.Price, request.CurrencyCode, _currencyLookup);
            var product = new Product(Guid.NewGuid(), request.Code, request.Name, price);
            _repository.AddProduct(product);

            return Unit.Value;
        }
    }
}