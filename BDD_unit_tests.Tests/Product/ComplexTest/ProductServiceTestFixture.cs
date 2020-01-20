using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.Tests.Helpers;
using BDD_unit_tests.User.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDD_unit_tests.Tests.Product.ComplexTest
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
            dbContext.SaveChanges();

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.Exist("existProduct")).Returns(true);
            productRepository.Setup(x => x.Get(1)).Returns(dbContext.Products.First());
            productRepository.Setup(x => x.Get(ProductCategory.Big)).Returns(new List<ProductModel>
            {
                new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct2", Cost = 2, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct3", Cost = 3, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct4", Cost = 4, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct5", Cost = 5, Category = ProductCategory.Big }
            });

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
            _action = () => _productService.Add(_currentUserId, _name, _cost, _category);
        }

        private void When_remove_product()
        {
            _action = () => _productService.Remove(_currentUserId, _productId);
        }

        private void When_update_product()
        {
            _action = () => _productService.Update(_currentUserId, _productId, _name, _cost, _category);
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

        private void Then_throw_no_exception()
        {
            _action();
        }
    }
}
