using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Contracts.DTO.ProductDTOs;
using OrderManagement.Core.Exceptions;
using OrderManagement.Core.Handlers.Commands;
using OrderManagement.Core.Handlers.Queries;
using System.Net;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve the list of all products.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Select/Search a specific product using name
        /// </summary>
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(string name)
        {
            try
            {
                var query = new GetProductByIdQuery(name);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Error = new string[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Add a new product to the database
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromBody] AddProductDTO model)
        {
            try
            {
                var command = new CreateProductCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);

            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Error = ex.Errors
                });
            }
        }

        /// <summary>
        /// Deletes a specific product by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _mediator.Send(new RemoveProductCommand { ProductId = id });
            return NoContent();
        }
    }
}
