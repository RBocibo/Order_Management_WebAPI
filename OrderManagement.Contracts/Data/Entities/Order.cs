using System;
using System.Collections.Generic;

namespace OrderManagement.Contracts.Entities
{
    /// <summary>
    /// Represents the current order
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Gets or sets the created date of the order.
        /// </summary>
        public DateTime CreatedDateUtc { get; set; }
        /// <summary>
        /// Gets or sets the quantity of the order.
        /// </summary>
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public int OrderStateId { get; set; }

        public virtual OrderState OrderState { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
