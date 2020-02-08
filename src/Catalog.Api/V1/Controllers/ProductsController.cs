using Catalog.Api.Controllers;
using Catalog.Api.Domain;
using Catalog.Api.Infrastructure;
using Catalog.Api.V1.Commands;
using Catalog.Api.V1.Dtos;
using Catalog.Api.V1.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get(string code, string name)
        {
            return await _mediator.Send(new ProductSearchQuery { Code = code, Name = name });
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(Guid id)
        {
            return await _mediator.Send(new ProductGetQuery { ProductId = id });
        }

        [HttpPost, Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromServices]IFileSaver fileSaver,
            [FromServices]IConfiguration configuration,
            [FromForm] ProductAddCommand command,
            ApiVersion version
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.PhotoName = await fileSaver.SaveAsync(command.Photo, configuration["Settings:UploadFolderName"]);
            var id = await _mediator.Send(command);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id, Version = $"{version}" }, new ProductDto { Id = id });
        }

        [HttpPut("{id:Guid}")]
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new ProductRemoveCommand { Id = id });
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}