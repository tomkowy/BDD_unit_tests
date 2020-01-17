using BDD_unit_tests.Common.Exceptions;
using BDD_unit_tests.Product.Exceptions;
using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using BDD_unit_tests.Product.Repository;
using BDD_unit_tests.User.Repository;
using System;
using System.Linq;

namespace BDD_unit_tests.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly BddDbContext _dbContext;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository, BddDbContext dbContext)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public void Add(int currentUserId, string name, int cost, string category)
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

            var categoryIsValid = Enum.TryParse<ProductCategory>(category, out ProductCategory categoryEnum);
            if (!categoryIsValid)
            {
                throw new ProductCategoryIsRequired();
            }

            if (_productRepository.Exist(name))
            {
                throw new ProductNameMustBeUnique();
            }

            var productsFromCategory = _productRepository.Get(categoryEnum);

            var costOfProductsFromCategory = productsFromCategory.Sum(x => x.Cost);
            if (costOfProductsFromCategory + cost > 100)
            {
                throw new CostOfProductsInCategoryException();
            }

            if (productsFromCategory.Count() > 4)
            {
                throw new NumberOfProductsInCategoryException();
            }

            var product = new ProductModel { Name = name, Cost = cost, Category = categoryEnum };
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

        public void Update(int currentUserId, int id, string name, int cost, string category)
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

            var categoryIsValid = Enum.TryParse<ProductCategory>(category, out ProductCategory categoryEnum);
            if (!categoryIsValid)
            {
                throw new ProductCategoryIsRequired();
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

            var productsFromCategory = _dbContext.Products.Where(x => x.Category == categoryEnum);

            var costOfProductsFromCategory = productsFromCategory.Sum(x => x.Cost);
            if (costOfProductsFromCategory + cost > 100)
            {
                throw new CostOfProductsInCategoryException();
            }

            if (productsFromCategory.Count() > 5)
            {
                throw new NumberOfProductsInCategoryException();
            }

            product.Name = name;
            product.Cost = cost;
            _dbContext.SaveChanges();
        }
    }
}
