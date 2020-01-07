using BDD_unit_tests.Product.ORM;
using System.Linq;

namespace BDD_unit_tests.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BddDbContext _dbContext;

        public UserRepository()
        {
            _dbContext = new BddDbContext();
        }

        public bool IsAdmin(int userId)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.UserId == userId);
            return user != null && user.Type == Models.UserType.Admin;
        }
        public bool IsModerator(int userId)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.UserId == userId);
            return user != null && user.Type == Models.UserType.Moderator;
        }
    }
}
