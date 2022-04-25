using FluentValidation;
using OrderManagement.Contracts.DTO.StockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
    public class StockDTOValidator : AbstractValidator<StockDTO>
    {
        public StockDTOValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage($"Product ID is required");
            RuleFor(x => x.AvailableStock).NotEmpty().WithMessage("Available stock is required");
        }
    }
}