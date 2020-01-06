namespace BDD_unit_tests.Product.Repository
{
    public interface IProductRepository
    {
        bool Exist(string name);
        bool Exist(int id);
    }
}
