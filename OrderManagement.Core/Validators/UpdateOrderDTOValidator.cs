using FluentValidation;
using OrderManagement.Contracts.DTO.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
            RuleFor(x => x.OrderID).NotEmpty().WithMessage("Order ID is required");
            RuleFor(x => x.OrderStatus).NotEmpty().WithMessage("Order status is required");
          
        }
    }
}
