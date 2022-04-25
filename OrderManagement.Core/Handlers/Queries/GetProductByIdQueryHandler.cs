using AutoMapper;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        //public int ProductId { get; }
        public string Name { get; set; }
        public GetProductByIdQuery(string name)
        {
            Name = name;
        }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.Product.GetAsync(a => a.Name == request.Name);
            if (product == null)
            {
                throw new EntityNotFoundException($"No product found with the name {request.Name }");
            }
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
