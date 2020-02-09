using Catalog.Api.Controllers;
using Catalog.Api.Domain;
using Catalog.Api.Infrastructure;
using Catalog.Api.Infrastructure.Exporter;
using Catalog.Api.Resources;
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

        [HttpGet("export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string exportType)
        {
            var exporter = new DataExporterFactory().GetExporter(exportType);
            if (exporter == null)
            {
                ModelState.AddModelError("ExportTye", string.Format(ErrorMessagesResource.InvalidExportTypeError, exportType));
                var problemDetails = new ValidationProblemDetails(ModelState);
                return BadRequest(problemDetails);
            }

            var products = await _mediator.Send(new ProductSearchQuery());
            var export = exporter.Export(products);

            return File(export.Stream, export.ContentType, export.FileName);
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

            if (command.Photo != null && command.Photo.Length > 0)
                command.PhotoName = await fileSaver.SaveAsync(command.Photo, configuration["Settings:ProductPhotoDirectory"]);

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