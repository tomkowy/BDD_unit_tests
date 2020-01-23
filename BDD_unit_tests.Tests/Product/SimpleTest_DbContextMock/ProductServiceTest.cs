using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace BDD_unit_tests.Tests.Product.SimpleTest_DbContextMock
{
    public partial class ProductServiceTest
    {

        [Scenario]
        public void Remove_product()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_product_to_db_context,
                Given_existing_product_id_from_db_context,
                When_remove_product,
                Then_throw_no_exception
                );
        }
    }
}
