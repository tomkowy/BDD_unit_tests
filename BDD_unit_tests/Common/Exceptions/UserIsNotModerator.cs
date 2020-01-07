using System;

namespace BDD_unit_tests.Common.Exceptions
{
    public class UserIsNotModerator : Exception
    {
        public UserIsNotModerator() : base("User is not Moderator")
        {
        }
    }
}
