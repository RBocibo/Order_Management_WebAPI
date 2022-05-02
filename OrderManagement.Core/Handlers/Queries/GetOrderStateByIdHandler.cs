using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderStatesDTO;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetOrderStateByIdQuery : IRequest<OrderStateDTO>
    {
        public int Id { get; }
        public GetOrderStateByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetOrderStateByIdHandler : IRequestHandler<GetOrderStateByIdQuery, OrderStateDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetOrderStateByIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderStateDTO> Handle(GetOrderStateByIdQuery request, CancellationToken cancellationToken)
        {
            //var orderState = await _repository.OrderState.GetAsync(a => a.OrderStateId == request.OrderStateId);
            var orderState = await _repository.OrderState.GetCachedOrderStatesByKey(request.Id);
            if (orderState == null)
            {
                throw new EntityNotFoundException($"No stateorder found with the name {request.Id }");
            }
            return _mapper.Map<OrderStateDTO>(orderState);
        }
    }
}
