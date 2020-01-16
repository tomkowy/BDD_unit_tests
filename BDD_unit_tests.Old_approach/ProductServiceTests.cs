using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Old_approach.Helpers;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.User.Models;
using BDD_unit_tests.User.Repository;
using Moq;
using System.Linq;
using Xunit;

namespace BDD_unit_tests.Old_approach
{
    public class ProductServiceTests : TestBase
    {
        private readonly BddDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductServiceTests()
        {
            _dbContext = GetDbContext();

            _dbContext.Add(new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big });
            _dbContext.Add(new ProductModel { Name = "existProduct2", Cost = 2, Category = ProductCategory.Big });
            _dbContext.Add(new ProductModel { Name = "existProduct3", Cost = 3, Category = ProductCategory.Big });
            _dbContext.Add(new ProductModel { Name = "existProduct4", Cost = 4, Category = ProductCategory.Big });
            _dbContext.Add(new ProductModel { Name = "existProduct5", Cost = 5, Category = ProductCategory.Big });

            _dbContext.SaveChanges();

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.Exist("existProduct")).Returns(true);
            productRepository.Setup(x => x.Get(1)).Returns(_dbContext.Products.First());

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.IsAdmin(1)).Returns(true);
            userRepository.Setup(x => x.IsModerator(2)).Returns(true);

            _userRepository = userRepository.Object;
            _productRepository = productRepository.Object;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Throw_exception_when_product_name_is_empty_or_null(string name)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, name, 50, "Small");

            Assert.Throws<ProductNameCannotBeEmptyException>(action);
        }

        [Fact]
        public void Throw_exception_when_adding_user_is_not_admin()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(3, "name", 50, "Small");

            Assert.Throws<UserIsNotAdmin>(action);
        }

        [Fact]
        public void Throw_exception_when_product_name_exist()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, "existProduct", 50, "Small");

            Assert.Throws<ProductNameMustBeUnique>(action);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Throw_exception_when_product_cost_is_lower_or_equal_zero(int cost)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, "name", cost, "Small");

            Assert.Throws<ProductCostMustBeGreaterThanZeroException>(action);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("wrongCategory")]
        public void Throw_exception_when_product_category_is_empty_or_null_or_wrong(string category)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, "name", 50, category);

            Assert.Throws<ProductCategoryIsRequired>(action);
        }

        [Fact]
        public void Throw_exception_when_products_cost_in_category_is_greater_one_hundred()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, "name", 101, "Small");

            Assert.Throws<CostOfProductsInCategoryException>(action);
        }

        [Fact]
        public void Throw_exception_when_products_count_is_greater_than_five()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Add(1, "name", 1, "Big");

            Assert.Throws<NumberOfProductsInCategoryException>(action);
        }

        [Fact]
        public void Add_product()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            productService.Add(1, "productName", 50, "Small");
        }

        [Fact]
        public void Remove_product()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            productService.Remove(1, 1);
        }

        [Fact]
        public void Throw_exception_when_removing_user_is_not_admin()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Remove(2, 1);

            Assert.Throws<UserIsNotAdmin>(action);
        }

        [Fact]
        public void Throw_exception_when_removing_product_not_exist()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Remove(1, 10);

            Assert.Throws<ProductDoesNotExistException>(action);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Throw_exception_when_updating_product_name_is_empty_or_null(string name)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 1, name, 50, "Small");

            Assert.Throws<ProductNameCannotBeEmptyException>(action);
        }

        [Fact]
        public void Throw_exception_when_updating_user_is_not_moderator()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(3, 1, "name", 50, "Small");

            Assert.Throws<UserIsNotModerator>(action);
        }

        [Fact]
        public void Throw_exception_when_updating_product_name_exist()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 1, "existProduct", 50, "Small");

            Assert.Throws<ProductNameMustBeUnique>(action);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Throw_exception_when_updating_product_cost_is_lower_or_equal_zero(int cost)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 1, "name", cost, "Small");

            Assert.Throws<ProductCostMustBeGreaterThanZeroException>(action);
        }

        [Fact]
        public void Throw_exception_when_updating_product_not_exist()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 10, "name", 50, "Small");

            Assert.Throws<ProductDoesNotExistException>(action);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("wrongCategory")]
        public void Throw_exception_when_updating_product_category_is_empty_or_null_or_wrong(string category)
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 10, "name", 15000, category);

            Assert.Throws<ProductCategoryIsRequired>(action);
        }

        [Fact]
        public void Throw_exception_when_updating_products_cost_in_category_is_greater_one_hundred()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            void action() => productService.Update(2, 1, "productName", 101, "Small");

            Assert.Throws<CostOfProductsInCategoryException>(action);
        }

        [Fact]
        public void Update_product()
        {
            var productService = new ProductService(_productRepository, _userRepository, _dbContext);

            productService.Update(2, 1, "productName", 50, "Small");
        }
    }
}
