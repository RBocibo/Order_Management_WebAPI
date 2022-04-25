using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class RemoveOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
    public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand, Unit>
    {
        private readonly IUnitOfWork _repository;
        public RemoveOrderCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.Order.GetAsync(a => a.OrderId == request.OrderId);
            if (order == null)
            {
                throw new EntityNotFoundException($"No order found with the name {request.OrderId }");
            }
            else
            {
                await _repository.Order.DeleteAsync(request.OrderId);
                await _repository.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
