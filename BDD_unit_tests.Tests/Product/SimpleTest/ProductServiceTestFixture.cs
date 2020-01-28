using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.Product.Services;
using BDD_unit_tests.Tests.Helpers;
using BDD_unit_tests.User.Repository;
using LightBDD.XUnit2;
using Moq;
using System;
using Xunit;

[assembly: LightBddScopeAttribute]

namespace BDD_unit_tests.Tests.Product.SimpleTest
{
    public partial class ProductServiceTest : TestBase
    {
        private int _currentUserId;
        private string _name;
        private int _cost;
        private string _category;

        private IProductService _productService;

        private Exception _exception;
        private int _result;

        public ProductServiceTest()
        {
            var dbContext = GetDbContext();

            var productRepository = new Mock<IProductRepository>();

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.IsModerator(2)).Returns(true);

            _productService = new ProductService(productRepository.Object, userRepository.Object, dbContext);
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
