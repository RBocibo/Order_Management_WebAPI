using System;

public class EntitiesMock
{
    public ICollection<Product> GetTestProducts()
    {
        return new List<Product>
      {
        new Product
        {
         ProductId = 1,
         Name = "Azure fundamentals",
         Description = "Azure fundamentals",
         Price = 250
        },
        new Product
        {
         ProductId = 2,
         Name = "Entity framework",
         Description = "Entity framework",
         Price = 250
        },
        };
    }

    public ICollection<Order> GetTestOrders()
    {
        return new Order>
      {
        new Order
        {
         OrderId = 1,
         ProductId = 1,
         Name = "Azure fundamentals-",
         CreatedDateUtc = DateTime.UtcNow,
         Quantity = 10
        },
        new Order
        {
         OrderId = 2,
         ProductId = 1,
         Name = "Azure fundamentals",
         CreatedDateUtc = DateTime.UtcNow,
         Quantity = 22
        },
      };
    }

    public ICollection<Stock> GetTestStock()
    {
        return new List<Stock>
      {
        new Stock
        {
         StockId = 1,
         ProductId = 1,
         AvailableStock = 100
        },
        new Stock
        {
         StockId = 2,
         ProductId = 2,
         AvailableStock = 50
        }
      };
    }

    public ICollection<OrderState> GetTestOrderStates()
    {
        return new List<OrderState>
      {
        new OrderState
        {
         OrderStateId = 1,
         State = "Reserved"
        },
       new OrderState
        {
         OrderStateId = 2,
         State = "Cancelled"
        },
       new OrderState
        {
         OrderStateId = 3,
         State = "Completed"
        },
       };
    }
}
