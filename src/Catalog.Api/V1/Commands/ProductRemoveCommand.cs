using MediatR;
using System;

namespace Catalog.Api.V1.Commands
{
    public class ProductRemoveCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}