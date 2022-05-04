using OrderManagement.Contracts.Entities;
using OrderManagement.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;

namespace Tests.RepositoryTests
{
    public class RepositoryTest
    {
        private readonly EntitiesDBMockContext _entitiesDbMockContext;

        public RepositoryTest()
        {
            // _productsDBContext = new Data.Products.Context.ProductsDBContext();
            _entitiesDbMockContext = new EntitiesDBMockContext();

        }


        [Fact]
        public void TestCreateDBOrders()
        {
            var dbContext = _entitiesDbMockContext.GetDbContext();
            var repo = new Repository<Order>(dbContext);

            var preOrderCreationCount = dbContext.Orders.Count();

            var newOrder = new Order
            {
                OrderId = 11,
                ProductId = 10,
                Name = "My Order",
                Quantity = 45,
                CreatedDateUtc = DateTime.UtcNow,
                OrderStateId = 1
            };

            repo.AddAsync(newOrder);
            //repo.Save();

            var postOrderCreationCount = dbContext.Orders.Count();

            Assert.NotEqual(preOrderCreationCount, postOrderCreationCount);
            Assert.Equal(preOrderCreationCount + 1, postOrderCreationCount);

            var order = repo.GetAsync(a => a.OrderId == newOrder.OrderId);

            Assert.NotNull(order);
            Assert.Equal(newOrder.OrderId, order.Id);

            var orders = repo.GetAsync(x => x.OrderId == newOrder.OrderId);

            Assert.NotNull(orders);
        }

        [Fact]
        public void TestUpdateDBOrders()
        {
            var dbContext = _entitiesDbMockContext.GetDbContext();
            var repo = new Repository<Order>(dbContext);

            var existingOrder = dbContext.Orders.FirstOrDefault();
            Assert.NotNull(existingOrder);

            string updatedOrderName = "Order updated";
            int updateOrderQuantity = 15;

            Assert.NotEqual(updatedOrderName, existingOrder.Name);
            Assert.NotEqual(updateOrderQuantity, existingOrder.Quantity);

            existingOrder.Name = updatedOrderName;
            existingOrder.Quantity = updateOrderQuantity;

            repo.UpdateAsync(existingOrder);
            //repo.Save();

            var updateOrder = repo.GetAsync(a => a.OrderId == existingOrder.OrderId);

            //Assert.Equal(updatedOrderName, updateOrder.Name);
            //Assert.Equal(updateOrderQuantity, updateOrder.Quantity);

        }

        [Fact]
        public void TestDeleteDBOrders()
        {
            var dbContext = _entitiesDbMockContext.GetDbContext();
            var repo = new Repository<Order>(dbContext);

            var existingOrder = dbContext.Orders.FirstOrDefault();
            Assert.NotNull(existingOrder);

            repo.DeleteAsync(existingOrder);
            //repo.Save();

            var deletedOrder = repo.GetAsync(a => a.OrderId == existingOrder.OrderId);

            Assert.True(deletedOrder is null);

        }


        [Fact]
        public void TestCreateDBProducts()
        {
            var dbContext = _entitiesDbMockContext.GetDbContext();
            var repo = new Repository<Product>(dbContext);

            var preProductCreationCount = dbContext.Products.Count();

            var product = new Product
            {
                ProductId = 10,
                Description = "Microservices",
                Name = "Microservices",
                Price = 45
            };

            repo.AddAsync(product);
            //repo.Save();
            

            var postProductCreationCount = dbContext.Products.Count();

            Assert.NotEqual(preProductCreationCount, postProductCreationCount);
            Assert.Equal(preProductCreationCount + 1, postProductCreationCount);
        }
    }
}
