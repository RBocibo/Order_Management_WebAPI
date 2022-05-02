using System;

public class DbContextMockBase<TestDbContext> where TestDbContext : DbContext
{
    public abstract TestDbContext GetDbContext();
    public abstract void PopulateEntities(TestDbContext dbContext);
}
