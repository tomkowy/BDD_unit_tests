namespace BDD_unit_tests.Product.Services
{
    public interface IProductService
    {
        void Add(int currentUserId, string name, int cost);
        void Update(int currentUserId, int id, string name, int cost);
        void Remove(int currentUserId, int id);
    }
}
