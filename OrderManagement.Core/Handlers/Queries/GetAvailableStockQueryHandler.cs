using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.StockDTOs;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetAvailableStockQuery : IRequest<StockDTO>
    {
        public int ProductId { get; }
        //public string Name { get; set; }
        public GetAvailableStockQuery(int productId)
        {
            ProductId = productId;
        }
    }
    public class GetAvailableStockQueryHandler : IRequestHandler<GetAvailableStockQuery, StockDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAvailableStockQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StockDTO> Handle(GetAvailableStockQuery request, CancellationToken cancellationToken)
        {
            var avalableStock = await _repository.Stock.GetAsync(a => a.ProductId == request.ProductId);
            if (avalableStock == null)
            {
                throw new EntityNotFoundException($"No product found with the ID {request.ProductId }");
            }
            return _mapper.Map<StockDTO>(avalableStock);
        }
    }
}
