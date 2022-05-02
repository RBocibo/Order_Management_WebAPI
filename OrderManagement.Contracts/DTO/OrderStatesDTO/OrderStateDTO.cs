using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO.OrderStatesDTO
{
    public class OrderStateDTO
    {
        public int OrderStateId { get; set; }
        public string State { get; set; }
    }
}
