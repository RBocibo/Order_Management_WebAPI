using OrderManagement.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO.OrderDTOs
{
    public class UpdateOrderDTO
    {
        public int OrderID { get; set; }  
        public OrderStatus OrderStatus { get; set; }
    }
}
