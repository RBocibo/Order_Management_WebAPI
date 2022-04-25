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
    //public class GetOrderByDateQuery : IRequest<OrderDTO>
    //{
    //    //public int ProductId { get; }
    //    public DateTime Date { get; set; }
    //    public GetOrderByDateQuery(DateTime date)
    //    {
    //        Date = date;
    //    }
    //}
    //public class GetOrderByDateQueryHandler : IRequestHandler<GetOrderByNameQuery, OrderDTO>
    //{
    //    private readonly IUnitOfWork _repository;
    //    private readonly IMapper _mapper;

    //    public GetOrderByDateQueryHandler(IUnitOfWork repository, IMapper mapper)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //    }

    //    public async Task<OrderDTO> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
    //    {
    //        var order = await _repository.Order.GetAsync(a => a.CreatedDateUtc == request.Date);
    //        if (order == null)
    //        {
    //            throw new EntityNotFoundException($"No date found with the name {request.Name }");
    //        }
    //        return _mapper.Map<OrderDTO>(order);
    //    }
    //}
}

