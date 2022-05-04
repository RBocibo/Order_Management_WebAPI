using Moq;
using OrderManagement.Contracts.Data.Repositories;

namespace Tests.Mocks
{
	public class RepositoryMocks<T> where T : class
	{
		private readonly Mock<IOrderRepository> _orderRepositoryMock;
		private readonly Mock<IOrderStateRepository> _orderStateRepositoryMock;
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IStockRepository> _stockRepositoryMock;
		public RepositoryMocks()
		{
		}
	}
}