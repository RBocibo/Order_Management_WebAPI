using System;

public class EntitiesDBMockContext : DbContextMockBase<SouthWestTradersDbContext>
{
	private readonly EntitiesDBMockContext _context;

	public EntitiesDBMockContext()
	{
		_context = new EntitiesDBMockContext();
	}

	private override SouthWestTradersDbContext GetDbContext()
	{
		var mockDatabase = InitializeDb();
		PopulateEntities(mockDatabase);
		return mockDatabase;
	}

	public override void PopulateEntities(Data.Products.Context.ProductsDBContext productsDBContextMock)
	{
		productsDBContextMock.AddRange(_productsDbEntities.GetTestProducts());
		productsDBContextMock.AddRange(_productsDbEntities.GetTestOrders());
		productsDBContextMock.AddRange(_productsDbEntities.GetTestStock());
		productsDBContextMock.AddRange(_productsDbEntities.GetTestOrderStates());

		productsDBContextMock.SaveChanges();
	}

	private Data.Products.Context.ProductsDBContext InitializeDb()
	{
		var mockDb = new DbContextOptionsBuilder<Data.Products.Context.ProductsDBContext>()
		 .UseInMemoryDatabase("ProductsMockDB")
		 .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
		 .EnableDetailedErrors()
		 .EnableSensitiveDataLogging()
		 .Options;

		var context = new Data.Products.Context.ProductsDBContext(mockDb);

		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();

		return context;
	}
}
