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
    public class ProductCommandHandler :
        IRequestHandler<ProductAddCommand, Guid>,
        IRequestHandler<ProductEditCommand>,
        IRequestHandler<ProductRemoveCommand>
    {
        private readonly ICurrencyLookup _currencyLookup;
        private readonly IProductRepository _repository;

        public ProductCommandHandler(ICurrencyLookup currencyLookup, IProductRepository repository)
        {
            _currencyLookup = currencyLookup;
            _repository = repository;
        }

        public async Task<Guid> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.CheckProductExistByCodeAsync(request.Code))
                throw new DuplicateDomainException(string.Format(ErrorMessagesResource.DuplicateError, DisplayNamesResource.ProductCode));

            var price = Money.FromDecimal(request.Price, request.CurrencyCode, _currencyLookup);
            var product = new Product(Guid.NewGuid(), request.Code, request.Name, price);
            _repository.AddProduct(product);

            return product.Id;
        }

        public async Task<Unit> Handle(ProductEditCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);
            if (product == null)
                throw new NotFoundDomainException(string.Format(ErrorMessagesResource.NotFoundError, DisplayNamesResource.Product));

            if (product.Name.Value != request.Name)
                product.UpdateName(request.Name);

            if (product.Price.Amount != request.Price)
                product.UpdatePrice(Money.FromDecimal(request.Price, request.CurrencyCode, _currencyLookup));

            if (product.Code.Value != request.Code)
            {
                if (await _repository.CheckProductExistByCodeAsync(request.Code))
                    throw new DuplicateDomainException(string.Format(ErrorMessagesResource.DuplicateError, DisplayNamesResource.ProductCode));

                product.UpdateCode(request.Code);
            }

            _repository.EditProduct(product);

            return Unit.Value;
        }

        public async Task<Unit> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);
            if (product == null)
                throw new NotFoundDomainException(string.Format(ErrorMessagesResource.NotFoundError, DisplayNamesResource.Product));

            _repository.RemoveProduct(product);

            return Unit.Value;
        }
    }
}