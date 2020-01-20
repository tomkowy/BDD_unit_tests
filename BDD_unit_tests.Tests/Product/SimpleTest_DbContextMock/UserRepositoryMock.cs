using BDD_unit_tests.User.Repository;
using Moq;

namespace BDD_unit_tests.Tests.Product.SimpleTest_DbContextMock
{
    public class UserRepositoryMock : Mock<IUserRepository>
    {
        public UserRepositoryMock IsModeratorMock(int userId, bool output)
        {
            Setup(x => x.IsModerator(userId))
            .Returns(output);
            return this;
        }
        public UserRepositoryMock IsAdminMock(int userId, bool output)
        {
            Setup(x => x.IsAdmin(userId))
            .Returns(output);
            return this;
        }
    }
}
