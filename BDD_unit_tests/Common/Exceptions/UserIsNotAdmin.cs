using System;

namespace BDD_unit_tests.Common.Exceptions
{
    public class UserIsNotAdmin : Exception
    {
        public UserIsNotAdmin() : base("User is no admin")
        {
        }
    }
}
