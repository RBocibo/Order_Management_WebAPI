using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public abstract class DbContextMockBase<TestDbContext> where TestDbContext : DbContext
    {
        public abstract TestDbContext GetDbContext();
        public abstract void PopulateEntities(TestDbContext dbContext);
    }
}