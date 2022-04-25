using FluentValidation;
using OrderManagement.Contracts.DTO.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
    public class AddOrderDTOValidator : AbstractValidator<AddOrderDTO>
    {
        public AddOrderDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CreatedDateUtc).NotEmpty().WithMessage("Date created is required");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required");

        }
    }
}
