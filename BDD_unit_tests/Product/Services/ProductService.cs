using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.User.Repository;

namespace BDD_unit_tests.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly BddDbContext _dbContext;

        public ProductService(BddDbContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new ProductReposiotry(dbContext);
            _userRepository = new UserRepository(dbContext);
        }

        public void Add(int currentUserId, string name, int cost)
        {
            if (!_userRepository.IsAdmin(currentUserId))
            {
                throw new UserIsNotAdmin();
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

            var product = new ProductModel { Name = name, Cost = cost };
            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }

        public void Remove(int currentUserId, int id)
        {
            if (!_userRepository.IsAdmin(currentUserId))
            {
                throw new UserIsNotAdmin();
            }

            var product = _productRepository.Get(id);
            if (product == null)
            {
                throw new ProductDoesNotExistException();
            }

            _dbContext.Remove(product);
            _dbContext.SaveChanges();
        }

        public void Update(int currentUserId, int id, string name, int cost)
        {
            if (!_userRepository.IsModerator(currentUserId))
            {
                throw new UserIsNotModerator();
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

            var product = _productRepository.Get(id);
            if (product == null)
            {
                throw new ProductDoesNotExistException();
            }

            product.Name = name;
            product.Cost = cost;
            _dbContext.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
