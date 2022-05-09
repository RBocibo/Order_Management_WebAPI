using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.OrderDTOs;
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
    [SwaggerTag("Provides operations to create orders")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all orders from the database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrdersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve an order using a name
        /// </summary>
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(typeof(OrderDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var query = new GetOrderByNameQuery(name);
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
        /// Retrieve an order using date
        /// </summary>
        //[HttpGet]
        //[Route("{date}")]
        //[ProducesResponseType(typeof(OrderDTO), (int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType(typeof(BaseResponseDTO))]
        //public async Task<IActionResult> GetByDate(DateTime date)
        //{
        //    try
        //    {
        //        var query = new GetOrderByDateQuery(date);
        //        var response = await _mediator.Send(query);
        //        return Ok(response);
        //    }
        //    catch (EntityNotFoundException ex)
        //    {
        //        return NotFound(new BaseResponseDTO
        //        {
        //            IsSuccess = false,
        //            Error = new string[] { ex.Message }
        //        });
        //    }
        //}

        /// <summary>
        /// Place an order
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromBody] AddOrderDTO model)
        {
            try
            {
                var command = new CreateOrderCommand(model);
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
        /// Cancel order
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDTO model)
        {
            try
            {
                var command = new CancelOrderCommand(model);
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
        /// Deletes an order using the ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _mediator.Send(new RemoveOrderCommand { OrderId = id });
            return NoContent();
        }
    }
}
