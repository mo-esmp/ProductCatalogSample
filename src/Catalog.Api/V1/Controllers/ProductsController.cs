﻿using Catalog.Api.Controllers;
using Catalog.Api.Domain;
using Catalog.Api.V1.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Catalog.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(ProductAddCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(command);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, ProductEditCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.Id = id;
            await _mediator.Send(command);
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}