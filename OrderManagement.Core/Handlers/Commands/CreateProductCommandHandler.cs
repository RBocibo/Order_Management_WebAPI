using FluentValidation;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.ProductDTOs;
using OrderManagement.Core.Exceptions;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public AddProductDTO Model { get; }
        public CreateProductCommand(AddProductDTO model)
        {
            Model = model;
        }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<AddProductDTO> _validator;
        public CreateProductCommandHandler(IUnitOfWork repository, IValidator<AddProductDTO> validator)
        {
            _repository = repository;
            _validator = validator; 
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            AddProductDTO model = request.Model;
            var product = _validator.Validate(model);
            if(!product.IsValid)
            {
                var errors = product.Errors.Select(e => e.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors,
                };
            }

            var entity = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
            };

            _repository.Product.AddAsync(entity);
            await _repository.CommitAsync();

            return entity.ProductId;
        }
    }
}
