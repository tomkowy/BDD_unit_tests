using Microsoft.EntityFrameworkCore;

namespace BDD_unit_tests.Product.ORM
{
    public class BddDbContext : DbContext
    {
        public DbSet<User.Models.User> Users { get; set; }
        public DbSet<Models.ProductModel> Products { get; set; }
    }
}
