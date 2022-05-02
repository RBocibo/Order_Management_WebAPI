using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderStatesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetAllOrderStatesQuery : IRequest<IEnumerable<OrderStateDTO>>
    {
    }

    public class GetAllOrderStatesHandler : IRequestHandler<GetAllOrderStatesQuery, IEnumerable<OrderStateDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllOrderStatesHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderStateDTO>> Handle(GetAllOrderStatesQuery request, CancellationToken cancellationToken)
        {
            var orderState = await _repository.OrderState.GetCachedOrderStates();
            return _mapper.Map<IEnumerable<OrderStateDTO>>(orderState);
            
        }
    }
}
