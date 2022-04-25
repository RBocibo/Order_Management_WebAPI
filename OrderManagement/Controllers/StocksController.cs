using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.DTO;
using OrderManagement.Contracts.DTO.StockDTOs;
using OrderManagement.Core.Exceptions;
using OrderManagement.Core.Handlers.Queries;
using System.Net;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
