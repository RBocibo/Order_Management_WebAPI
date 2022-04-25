using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderDTOs;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetOrderByNameQuery : IRequest<OrderDTO>
    {
        //public int ProductId { get; }
        public string Name { get; set; }
        public GetOrderByNameQuery(string name)
        {
            Name = name;
        }
    }
    public class GetOrderByNameQueryHandler : IRequestHandler<GetOrderByNameQuery, OrderDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetOrderByNameQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.Order.GetAsync(a => a.Name == request.Name);
            if (order == null)
            {
                throw new EntityNotFoundException($"No order found with the name {request.Name }");
            }
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
