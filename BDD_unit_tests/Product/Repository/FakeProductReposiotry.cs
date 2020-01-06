namespace BDD_unit_tests.Product.Repository
{
    public class FakeProductReposiotry : IProductRepository
    {
        public bool Exist(string name)
        {
            if (name == "existName")
            {
                return true;
            }
            return false;
        }
        public bool Exist(int id)
        {
            if (id == 1)
            {
                return true;
            }
            return false;
        }
    }
}
