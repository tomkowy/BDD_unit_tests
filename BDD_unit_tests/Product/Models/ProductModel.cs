namespace BDD_unit_tests.Product.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public ProductCategory Category { get; set; }
    }
}
