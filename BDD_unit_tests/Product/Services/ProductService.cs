using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Repository;

namespace BDD_unit_tests.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly FakeORM _fakeORM;

        public ProductService()
        {
            _productRepository = new FakeProductReposiotry();
            _fakeORM = new FakeORM();
        }

        public void Add(string name, int cost)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ProductNameCannotBeEmptyException();
            }

            if (cost <= 0)
            {
                throw new ProductCostMustBeGreaterThanZeroException();
            }

            if (_productRepository.Exist(name))
            {
                throw new ProductNameMustBeUnique();
            }

            _fakeORM.Add();
        }

        public void Remove(int id)
        {
            if (!_productRepository.Exist(id))
            {
                throw new ProductDoesNotExistException();
            }

            _fakeORM.Remove();
        }

        public void Update(int id, string name, int cost)
        {
            if (!_productRepository.Exist(id))
            {
                throw new ProductDoesNotExistException();
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ProductNameCannotBeEmptyException();
            }

            if (cost <= 0)
            {
                throw new ProductCostMustBeGreaterThanZeroException();
            }

            if (_productRepository.Exist(name))
            {
                throw new ProductNameMustBeUnique();
            }

            _fakeORM.Update();
        }
    }
}
