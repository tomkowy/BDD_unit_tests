namespace BDD_unit_tests.Product.Services
{
    public interface IProductService
    {
        void Add(int currentUserId, string name, 
            int cost, string category);
        void Update(int currentUserId, int id, 
            string name, int cost, string category);
        void Remove(int currentUserId, int id);
    }
}
