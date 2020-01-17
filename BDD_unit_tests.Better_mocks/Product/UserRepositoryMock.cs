using BDD_unit_tests.User.Repository;
using Moq;

namespace BDD_unit_tests.Better_mocks.Product
{
    public class UserRepositoryMock : Mock<IUserRepository>
    {
        public UserRepositoryMock IsAdminMock(int userId, bool output)
        {
            Setup(x => x.IsAdmin(userId))
            .Returns(output);
            return this;
        }

        public UserRepositoryMock IsModeratorMock(int userId, bool output)
        {
            Setup(x => x.IsModerator(userId))
            .Returns(output);
            return this;
        }
    }
}
