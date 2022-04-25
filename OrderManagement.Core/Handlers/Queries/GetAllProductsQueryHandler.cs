using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductDTO>>
    {

    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Product.ListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(entities);
        }
    }
}
