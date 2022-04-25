using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderStateId { get; set; }

       // public virtual OrderState OrderState { get; set; } = null!;
       // public virtual Product Product { get; set; } = null!;
    }
}
