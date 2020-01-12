using BDD_unit_tests.Product.ORM;
using LightBDD.XUnit2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Xunit.Abstractions;

namespace BDD_unit_tests.First_approach.Helpers
{
    public class TestBase : FeatureFixture
    {
        protected TestBase()
        {
        }

        protected TestBase(ITestOutputHelper output) : base(output)
        {
        }

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
