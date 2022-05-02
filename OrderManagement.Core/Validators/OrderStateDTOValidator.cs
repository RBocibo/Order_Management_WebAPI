using FluentValidation;
using OrderManagement.Contracts.DTO.OrderStatesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
    public class OrderStateDTOValidator : AbstractValidator<OrderStateDTO>
    {
        public OrderStateDTOValidator()
        {
            RuleFor(x => x.OrderStateId).NotEmpty().WithMessage($"OrderState ID is required");
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required");
        }
    }
}
