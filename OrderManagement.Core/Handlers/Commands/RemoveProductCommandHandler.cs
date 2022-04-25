using AutoMapper;
using FluentValidation;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public int ProductId { get; set; }
    }
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IUnitOfWork _repository;
        public RemoveProductCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.Product.GetAsync(a => a.ProductId == request.ProductId);
            if (product == null)
            {
                throw new EntityNotFoundException($"No product found with the name {request.ProductId }");
            }
            else
            {
                await _repository.Product.DeleteAsync(request.ProductId);
                await _repository.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
