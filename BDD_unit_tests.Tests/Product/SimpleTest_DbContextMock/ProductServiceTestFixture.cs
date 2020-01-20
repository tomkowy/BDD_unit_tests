using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.Tests.Helpers;
using System;
using System.Linq;

namespace BDD_unit_tests.Tests.Product.SimpleTest_DbContextMock
{
    public partial class ProductServiceTest : TestBase
    {
        private int _currentUserId;
        private string _name;
        private int _cost;
        private string _category;
        private int _productId;

        private Action _action;

        private readonly ProductRepositoryMock _productRepositoryMock = new ProductRepositoryMock();
        private readonly UserRepositoryMock _userRepositoryMock = new UserRepositoryMock();
        private IProductService _productService;
        private readonly BddDbContext _dbContext;

        public ProductServiceTest()
        {
            _dbContext = GetDbContext();

            _productService = new ProductService(_productRepositoryMock.Object, _userRepositoryMock.Object, _dbContext);
        }

        private void Give_current_user_as_admin()
        {
            _currentUserId = 1;
            _userRepositoryMock.IsAdminMock(_currentUserId, true);
        }

        private void Give_product_to_db_context()
        {
            _dbContext.Add(new ProductModel { Name = "existProduct1", Cost = 1, Category = ProductCategory.Big });
            _dbContext.SaveChanges();
        }

        private void Give_existing_product_id_from_db_context()
        {
            _productId = 1;
            _productRepositoryMock.GetMock(_productId, _dbContext.Products.First());
        }

        private void When_remove_product()
        {
            _action = () => _productService.Remove(_currentUserId, _productId);
        }

        private void Then_throw_no_exception()
        {
            _action();
        }
    }
}
