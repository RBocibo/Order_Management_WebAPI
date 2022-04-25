using System;
using System.Collections.Generic;

namespace OrderManagement.Contracts.Entities
{
    public partial class OrderState
    {
        public OrderState()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderStateId { get; set; }
        public string State { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
