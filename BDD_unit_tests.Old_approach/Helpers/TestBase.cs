using BDD_unit_tests.Product.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace BDD_unit_tests.Old_approach.Helpers
{
    public abstract class TestBase
    {
        public BddDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<BddDbContext>();

            InMemoryDatabaseRoot _inMemoryDatabaseRoot = new InMemoryDatabaseRoot();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString(), _inMemoryDatabaseRoot)
                .EnableServiceProviderCaching(false);

            var dbContext = new BddDbContext(builder.Options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
