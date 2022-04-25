using OrderManagement.Contracts.Enums;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO.OrderDTOs
{
    public class AddOrderDTO
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
