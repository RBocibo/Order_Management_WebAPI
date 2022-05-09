using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.StockDTOs;
using OrderManagement.Core.Exceptions;
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
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieve available stock using product ID
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(StockDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            //Thread.Sleep(10000);
            try
            {
                var query = new GetAvailableStockQuery(id);
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
    }
}
