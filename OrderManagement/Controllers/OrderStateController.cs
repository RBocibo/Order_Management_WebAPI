using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.OrderStatesDTO;
using OrderManagement.Core.Exceptions;
using OrderManagement.Core.Handlers.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Provides operations to manage order states
    /// </summary>
    
    [ApiController]
    [SwaggerTag("Provides operations to manage order states")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderStateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderStateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a set of order states
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderStateDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrderStatesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the specified order state
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderStateDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetOrderStateByIdQuery(id);
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
