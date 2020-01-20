using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace BDD_unit_tests.Tests.Product.SimpleTest_BetterSetup
{
    public partial class ProductServiceTest
    {
        [Scenario]
        public void Cannot_add_product_when_user_is_not_admin()
        {
            Runner.RunScenario(
                Give_moderator,
                When_add_product,
                Then_throw_user_is_not_admin_exception
                );
        }
    }
}
