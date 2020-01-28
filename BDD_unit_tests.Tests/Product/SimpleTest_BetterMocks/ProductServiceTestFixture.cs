using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.Tests.Helpers;
using System;
using Xunit;

namespace BDD_unit_tests.Tests.Product.SimpleTest_BetterMocks
{
    public partial class ProductServiceTest : TestBase
    {
        private int _currentUserId;
        private string _name;
        private int _cost;
        private string _category;

        private Exception _exception;
        private int _result;

        private IProductService _productService;

        public ProductServiceTest()
        {
            var dbContext = GetDbContext();

            var productRepositoryMock = new ProductRepositoryMock();

            var userRepositoryMock = new UserRepositoryMock();
            userRepositoryMock.IsModeratorMock(_currentUserId, true);

            _productService = new ProductService(productRepositoryMock.Object,
                userRepositoryMock.Object, dbContext);
        }

        private void Given_moderator()
        {
            _currentUserId = 2;
        }

        private void When_add_product()
        {
            try
            {
                _result = _productService.Add(_currentUserId, _name, _cost, _category);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        private void Then_throw_user_is_not_admin_exception()
        {
            Assert.IsType<UserIsNotAdmin>(_exception);
        }
    }
}
