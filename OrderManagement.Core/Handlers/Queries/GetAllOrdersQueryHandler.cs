using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {

    }
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Order.ListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(entities);
        }
    }
}
