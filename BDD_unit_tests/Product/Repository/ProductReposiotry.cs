using BDD_unit_tests.Product.Models;
using BDD_unit_tests.Product.ORM;
using System.Linq;

namespace BDD_unit_tests.Product.Repository
{
    public class ProductReposiotry : IProductRepository
    {
        private readonly BddDbContext _dbContext;

        public ProductReposiotry(BddDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exist(string name)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Name == name);
            return product != null;
        }
        public bool Exist(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            return product != null;
        }

        public ProductModel Get(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);
            return product;
        }
    }
}
