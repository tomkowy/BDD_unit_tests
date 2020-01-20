using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.Repository;
using Moq;
using System.Collections.Generic;

namespace BDD_unit_tests.Tests.Product.ComplexTest_BetterSetup
{
    public class ProductRepositoryMock : Mock<IProductRepository>
    {
        public ProductRepositoryMock ExistMock(string name, bool output)
        {
            Setup(x => x.Exist(name))
                .Returns(output);
            return this;
        }

        public ProductRepositoryMock GetMock(int id, ProductModel output)
        {
            Setup(x => x.Get(id))
                .Returns(output);
            return this;
        }

        public ProductRepositoryMock GetMock(ProductCategory category, IEnumerable<ProductModel> output)
        {
            Setup(x => x.Get(category))
                .Returns(output);
            return this;
        }
    }
}
