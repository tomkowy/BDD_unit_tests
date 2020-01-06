namespace BDD_unit_tests.Product.Services
{
    public interface IProductService
    {
        void Add(string name, int cost);
        void Update(int id, string name, int cost);
        void Remove(int id);
    }
}
