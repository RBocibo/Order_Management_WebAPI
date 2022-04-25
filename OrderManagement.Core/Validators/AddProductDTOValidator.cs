using FluentValidation;
using OrderManagement.Contracts.DTO.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
    public class AddProductDTOValidator : AbstractValidator<AddProductDTO>
    {
        public AddProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");

        }
    }
}
