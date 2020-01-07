using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Old_approach.Helpers;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.User.Models;
using Xunit;

namespace BDD_unit_tests.Old_approach
{
    public class ProductServiceTests : TestBase
    {
        private BddDbContext _dbContext;

        public ProductServiceTests()
        {
            _dbContext = GetDbContext();

            _dbContext.Add(new UserModel { Name = "Admin", Type = UserType.Admin });

            _dbContext.Add(new ProductModel { Name = "existProduct", Cost = 100 });

            _dbContext.SaveChanges();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Throw_exception_when_product_name_is_empty_or_null(string name)
        {
            var productService = new ProductService(_dbContext);

            void action() => productService.Add(1, name, 100);

            Assert.Throws<ProductNameCannotBeEmptyException>(action);
        }

        [Fact]
        public void Throw_exception_when_adding_user_is_not_admin()
        {
            var productService = new ProductService(_dbContext);

            void action() => productService.Add(3, "name", 100);

            Assert.Throws<UserIsNotAdmin>(action);
        }

        [Fact]
        public void Throw_exception_when_product_name_exist()
        {
            var productService = new ProductService(_dbContext);

            void action() => productService.Add(1, "existProduct", 100);

            Assert.Throws<ProductNameMustBeUnique>(action);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Throw_exception_when_product_cost_is_lower_or_equal_zero(int cost)
        {
            var productService = new ProductService(_dbContext);

            void action() => productService.Add(1, "name", cost);

            Assert.Throws<ProductCostMustBeGreaterThanZeroException>(action);
        }
    }
}
