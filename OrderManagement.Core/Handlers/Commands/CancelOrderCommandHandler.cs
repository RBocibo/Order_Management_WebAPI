using FluentValidation;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderDTOs;
using OrderManagement.Contracts.Enums;
using OrderManagement.Core.Exceptions;
using OrderManagement.Contracts.Entities;

namespace OrderManagement.Core.Handlers.Commands
{
    public class CancelOrderCommand : IRequest<int>
    {
        public UpdateOrderDTO Model { get; }
        public CancelOrderCommand(UpdateOrderDTO model)
        {
            Model = model;
        }
    }
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<UpdateOrderDTO> _validator;
        public CancelOrderCommandHandler(IUnitOfWork repository, IValidator<UpdateOrderDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<int> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            UpdateOrderDTO model = request.Model;
            var order = _validator.Validate(model);
            if (!order.IsValid)
            {
                var errors = order.Errors.Select(e => e.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors,
                };
            }

            var orderedItem = await _repository.Order.GetAsync(x => x.OrderId == model.OrderID);

            if ((OrderStatus)orderedItem.OrderStateId != OrderStatus.Completed)
            {
                orderedItem.OrderStateId = (int)OrderStatus.Cancelled;
                await _repository.Order.UpdateAsync(orderedItem);

                if (model.OrderStatus == OrderStatus.Cancelled)
                    await IncrementAvailableStock(orderedItem);

                await _repository.CommitAsync();

                return orderedItem.ProductId;

            }
            else
            {
                throw new OrderCannotBeCancelledException("Order cannot be cancelled");
            }
        }
        private async Task IncrementAvailableStock(Order orderedItem)
        {
            var stock = await _repository.Stock.GetAsync(x => x.ProductId == orderedItem.ProductId);
            stock.AvailableStock += orderedItem.Quantity;
            await _repository.Stock.UpdateAsync(stock);
        }
    }
}

