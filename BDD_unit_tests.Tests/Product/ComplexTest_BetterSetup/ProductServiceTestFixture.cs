using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDD_unit_tests.Tests.Product.ComplexTest_BetterSetup
{
    public partial class ProductServiceTest : TestBase
    {
        private int _currentUserId;
        private string _name;
        private int _cost;
        private string _category;
        private int _productId;

        private Action _action;

        private readonly IProductService _productService;
        private readonly UserRepositoryMock _userRepositoryMock = new UserRepositoryMock();
        private readonly ProductRepositoryMock _productRepositoryMock = new ProductRepositoryMock();
        private readonly BddDbContext _dbContext;

        public ProductServiceTest()
        {
            _dbContext = GetDbContext();
            _productService = new ProductService(_productRepositoryMock.Object, _userRepositoryMock.Object, _dbContext);
        }

        private void Given_current_user_as_admin()
        {
            _currentUserId = 1;
            _userRepositoryMock.IsAdminMock(_currentUserId, true);
        }

        private void Given_current_user_as_moderator()
        {
            _currentUserId = 2;
            _userRepositoryMock.IsModeratorMock(_currentUserId, true);
        }

        private void Given_empty_product_name()
        {
            _name = "";
        }

        private void Given_null_product_name()
        {
            _name = null;
        }

        private void Given_existing_product_name()
        {
            _name = "existProduct";
            _productRepositoryMock.ExistMock(_name, true);
        }

        private void Given_unique_product_name()
        {
            _name = "name";
        }

        private void Given_product_cost_greater_than_zero()
        {
            _cost = 2;
        }

        private void Given_product_cost_equal_zero()
        {
            _cost = 0;
        }

        private void Given_product_cost_lower_than_zero()
        {
            _cost = -1;
        }

        private void Given_product_cost_greater_than_hundred()
        {
            _cost = 101;
        }

        private void Given_product_category_small()
        {
            _category = ProductCategory.Small.ToString();
        }

        private void Given_product_category_big()
        {
            _category = ProductCategory.Big.ToString();
        }

        private void Given_empty_product_category()
        {
            _category = "";
        }

        private void Given_null_product_category()
        {
            _category = null;
        }

        private void Given_incorrect_product_category()
        {
            _category = "incorrectCategory";
        }

        private void Given_existing_product_id()
        {
            _productId = 1;
            _productRepositoryMock.GetMock(_productId, new ProductModel());
        }

        private void Given_not_existing_product_id()
        {
            _productId = 10;
            _productRepositoryMock.GetMock(_productId, null);
        }

        private void Given_five_products_from_category_big()
        {
            _productRepositoryMock.GetMock(ProductCategory.Big, new List<ProductModel> {
                new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct2", Cost = 2, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct3", Cost = 3, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct4", Cost = 4, Category = ProductCategory.Big },
                new ProductModel { Name = "existProduct5", Cost = 5, Category = ProductCategory.Big }
            });
        }

        private void Given_product_to_db_context()
        {
            _dbContext.Add(new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big });
            _dbContext.SaveChanges();
        }

        private void Given_existing_product_id_from_db_context()
        {
            _productId = 1;
            _productRepositoryMock.GetMock(_productId, _dbContext.Products.First());
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
