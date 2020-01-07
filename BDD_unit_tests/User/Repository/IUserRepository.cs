namespace BDD_unit_tests.User.Repository
{
    public interface IUserRepository
    {
        bool IsAdmin(int userId);
        bool IsModerator(int userId);
    }
}
