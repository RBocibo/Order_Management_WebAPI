using FluentValidation;
using MediatR;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.DTO.OrderDTOs;
using OrderManagement.Core.Exceptions;
using OrderManagement.Contracts.Enums;
using OrderManagement.Contracts.Entities;

namespace OrderManagement.Core.Handlers.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public AddOrderDTO Model { get; }
        public CreateOrderCommand(AddOrderDTO model)
        {
            Model = model;
        }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<AddOrderDTO> _validator;
        public CreateOrderCommandHandler(IUnitOfWork repository, IValidator<AddOrderDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            AddOrderDTO model = request.Model;
            var order = _validator.Validate(model);
            if (!order.IsValid)
            {
                var errors = order.Errors.Select(e => e.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors,
                };
            }

            var stock = await _repository.Stock.GetAsync(x => x.ProductId == model.ProductId);
           
            if (model.Quantity <= stock.AvailableStock)
            {

                var entity = new Order
                {
                    Name = model.Name,
                    CreatedDateUtc = model.CreatedDateUtc,
                    Quantity = model.Quantity,
                    ProductId = model.ProductId,
                    OrderStateId = (int)model.OrderStatus
                };

                await _repository.Order.AddAsync(entity);

                if (model.OrderStatus == OrderStatus.Reserved)
                    await DecrementAvailableStock(stock, entity);

                await _repository.CommitAsync();

                return entity.ProductId;
            }
            else
            {
                throw new UnitsOutOfStockException("Product Out Of stock");
            }  
        }

        private async Task DecrementAvailableStock(Stock stock, Order entity)
        {
            stock.AvailableStock -= entity.Quantity;
            await _repository.Stock.UpdateAsync(stock);
        }
    }
}
