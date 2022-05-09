using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Contracts.DTO.ProductDTOs;
using OrderManagement.Core.Authorization;
using OrderManagement.Core.Exceptions;
using OrderManagement.Core.Handlers.Commands;
using OrderManagement.Core.Handlers.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Provides operations to manage orders
    /// </summary>

    [ApiController]
    [SwaggerTag("Provides operations to manage products")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initialises the constructor
        /// </summary>
        /// <param name="_mediator"></param>
        
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a set of products
        /// </summary>
        /// <returns></returns>
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
        /// Retrieves the specified product
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// Creates a product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Policy = Policy.AdminAuthorizePolicy)]
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
        /// Deletes a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.AdminAuthorizePolicy)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _mediator.Send(new RemoveProductCommand { ProductId = id });
            return NoContent();
        }
    }
}
