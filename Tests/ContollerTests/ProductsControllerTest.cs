using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Contracts.DTO.StockDTOs;
using OrderManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;

namespace Tests.ContollerTests
{
    public class ProductsControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly EntitiesMock _entitiesMock;
             
        public ProductsControllerTest()
        {
            _mediator = new Mock<IMediator>();  
            _entitiesMock = new EntitiesMock();
        }
        
        [Fact]
        public async Task TestProductStockById()
        {
            //Arrange
            var productId = 1;
            var availableStock = _entitiesMock.GetDTOStocks().Where(x => x.ProductId == productId).FirstOrDefault();

            _mediator.Setup(a => a.Send(It.IsAny<IRequest<StockDTO>>(), default));//.Returns(Task.FromResult(availableStock));

            //Act
            var controller = new StocksController(_mediator.Object); 
            var result = await controller.GetById(productId);

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
    }
}