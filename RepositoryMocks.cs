using System;

public class RepositoryMocks<T> where T : class
{
	private readonly Mock<IOrderRepository> _orderRepositoryMock;
	private readonly Mock<IOrderSateRepository> _orderStateRepositoryMock;
	private readonly Mock<IProductRepository> _productRepositoryMock;
	private readonly Mock<IStockRepository> _stockRepositoryMock;
	public RepositoryMocks()
	{
	}
}
