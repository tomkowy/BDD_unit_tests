using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.First_approach.Helpers;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.User.Repository;
using LightBDD.XUnit2;
using Moq;
using System;
using Xunit;

[assembly: LightBddScopeAttribute]

namespace BDD_unit_tests.First_approach.Product
{
    public partial class ProductServiceTest : TestBase
    {
        private int _currentUserId;
        private string _name;
        private int _cost;
        private string _category;
        private int _productId;

        private Action _action;

        private IProductService _productService;

        public ProductServiceTest()
        {
            var dbContext = GetDbContext();
            dbContext.Add(new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big });
            dbContext.Add(new ProductModel { Name = "existProduct2", Cost = 2, Category = ProductCategory.Big });
            dbContext.Add(new ProductModel { Name = "existProduct3", Cost = 3, Category = ProductCategory.Big });
            dbContext.Add(new ProductModel { Name = "existProduct4", Cost = 4, Category = ProductCategory.Big });
            dbContext.Add(new ProductModel { Name = "existProduct5", Cost = 5, Category = ProductCategory.Big });
            dbContext.SaveChanges();

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.Exist("existProduct")).Returns(true);
            productRepository.Setup(x => x.Get(1)).Returns(new ProductModel());

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.IsAdmin(1)).Returns(true);
            userRepository.Setup(x => x.IsModerator(2)).Returns(true);

            _productService = new ProductService(productRepository.Object, userRepository.Object, dbContext);
        }

        private void Give_admin()
        {
            _currentUserId = 1;
        }

        private void Give_moderator()
        {
            _currentUserId = 2;
        }

        private void Give_empty_name()
        {
            _name = "";
        }

        private void Give_null_name()
        {
            _name = null;
        }

        private void Give_existing_name()
        {
            _name = "existProduct";
        }

        private void Give_name()
        {
            _name = "name";
        }

        private void Give_cost()
        {
            _cost = 2;
        }

        private void Give_cost_equal_zero()
        {
            _cost = 0;
        }

        private void Give_cost_lower_than_zero()
        {
            _cost = -1;
        }

        private void Give_cost_greater_than_hundred()
        {
            _cost = 101;
        }

        private void Give_category()
        {
            _category = ProductCategory.Small.ToString();
        }

        private void Give_category_big()
        {
            _category = ProductCategory.Big.ToString();
        }

        private void Give_empty_category()
        {
            _category = "";
        }

        private void Give_null_category()
        {
            _category = null;
        }

        private void Give_incorrect_category()
        {
            _category = "incorrectCategory";
        }

        private void Give_existing_product_id()
        {
            _productId = 1;
        }

        private void Give_not_existing_product_id()
        {
            _productId = 10;
        }

        private void When_add_product()
        {
            void action() => _productService.Add(_currentUserId, _name, _cost, _category);
            _action = action;
        }

        private void When_remove_product()
        {
            void action() => _productService.Remove(_currentUserId, _productId);
            _action = action;
        }

        private void When_update_product()
        {
            void action() => _productService.Update(_currentUserId, _productId, _name, _cost, _category);
            _action = action;
        }

        private void Then_throw_product_name_cannot_be_empty_exception()
        {
            Assert.Throws<ProductNameCannotBeEmptyException>(_action);
        }

        private void Then_throw_user_is_not_admin_exception()
        {
            Assert.Throws<UserIsNotAdmin>(_action);
        }

        private void Then_throw_product_name_must_be_unique_exception()
        {
            Assert.Throws<ProductNameMustBeUnique>(_action);
        }

        private void Then_throw_product_cost_must_be_greates_than_zero_exception()
        {
            Assert.Throws<ProductCostMustBeGreaterThanZeroException>(_action);
        }

        private void Then_throw_product_category_is_required_exception()
        {
            Assert.Throws<ProductCategoryIsRequired>(_action);
        }

        private void Then_throw_cost_of_products_in_category_exception()
        {
            Assert.Throws<CostOfProductsInCategoryException>(_action);
        }

        private void Then_throw_number_of_products_in_category_exception()
        {
            Assert.Throws<NumberOfProductsInCategoryException>(_action);
        }

        private void Then_throw_user_is_not_moderator_exception()
        {
            Assert.Throws<UserIsNotModerator>(_action);
        }

        private void Then_throw_product_does_not_exist_exception()
        {
            Assert.Throws<ProductDoesNotExistException>(_action);
        }
    }
}
