using BDD_unit_tests.Product.Models;
using System.Collections.Generic;

namespace BDD_unit_tests.Product.Repository
{
    public interface IProductRepository
    {
        bool Exist(string name);
        bool Exist(int id);
        ProductModel Get(int id);
        IEnumerable<ProductModel> Get();
    }
}
