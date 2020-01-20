using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.Repository;
using Moq;

namespace BDD_unit_tests.Tests.Product.SimpleTest_DbContextMock
{
    public class ProductRepositoryMock : Mock<IProductRepository>
    {
        public ProductRepositoryMock GetMock(int id, ProductModel output)
        {
            Setup(x => x.Get(id))
                .Returns(output);
            return this;
        }
    }
}
