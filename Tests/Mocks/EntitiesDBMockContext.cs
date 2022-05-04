using Microsoft.EntityFrameworkCore;
using OrderManagement.Contracts.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tests.Mocks
{
	public class EntitiesDBMockContext : DbContextMockBase<SouthWestTradersDbContext>
	{
		private readonly EntitiesMock _context;

		public EntitiesDBMockContext()
		{
			_context = new EntitiesMock();
		}

		public override SouthWestTradersDbContext GetDbContext()
		{
			var mockDatabase = InitializeDb();
			PopulateEntities(mockDatabase);
			return mockDatabase;
		}

		public override void PopulateEntities(SouthWestTradersDbContext productsDBContextMock)
		{
			productsDBContextMock.AddRange(_context.GetTestProducts());
			productsDBContextMock.AddRange(_context.GetTestOrders());
			//productsDBContextMock.AddRange(_context.GetTestStock());
			productsDBContextMock.AddRange(_context.GetTestOrderStates());

			productsDBContextMock.SaveChanges();
		}

		private SouthWestTradersDbContext InitializeDb()
		{
			//var mockDb = new DbContextOptionsBuilder<SouthWestTradersDbContext>()
			// .UseInMemoryDatabase("ProductsMockDB")
			// .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			// .EnableDetailedErrors()
			// .EnableSensitiveDataLogging()
			// .Options;

			var context = new SouthWestTradersDbContext();

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			return context;
		}
	}
}