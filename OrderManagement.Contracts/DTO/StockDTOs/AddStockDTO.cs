using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO.StockDTOs
{
    public class AddStockDTO
    {
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
