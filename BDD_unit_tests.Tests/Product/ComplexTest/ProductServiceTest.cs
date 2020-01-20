using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace BDD_unit_tests.Tests.Product.ComplexTest
{
    public partial class ProductServiceTest
    {
        [Scenario]
        public void Can_add_product()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_category,
                When_add_product
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_is_empty()
        {
            Runner.RunScenario(
                Give_admin,
                Give_empty_name,
                When_add_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_is_null()
        {
            Runner.RunScenario(
                Give_admin,
                Give_null_name,
                When_add_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_user_is_not_admin()
        {
            Runner.RunScenario(
                Give_moderator,
                When_add_product,
                Then_throw_user_is_not_admin_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_exist()
        {
            Runner.RunScenario(
                Give_admin,
                Give_existing_name,
                Give_cost,
                Give_category,
                When_add_product,
                Then_throw_product_name_must_be_unique_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_cost_is_lower_than_zero()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost_lower_than_zero,
                When_add_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_cost_is_equal_zero()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost_equal_zero,
                When_add_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_empty()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_empty_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_null()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_null_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_incorrect()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_incorrect_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_products_cost_in_category_is_greater_one_hundred()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost_greater_than_hundred,
                Give_category,
                When_add_product,
                Then_throw_cost_of_products_in_category_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_products_count_in_category_is_greater_than_five()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_category_big,
                When_add_product,
                Then_throw_number_of_products_in_category_exception
                );
        }

        [Scenario]
        public void Remove_product()
        {
            Runner.RunScenario(
                Give_admin,
                Give_existing_product_id,
                When_remove_product,
                Then_throw_no_exception
                );
        }

        [Scenario]
        public void Cannot_remove_product_when_user_is_not_admin()
        {
            Runner.RunScenario(
                Give_moderator,
                When_remove_product
                );
        }

        [Scenario]
        public void Cannot_remove_product_when_product_not_exist()
        {
            Runner.RunScenario(
                Give_admin,
                Give_not_existing_product_id,
                When_remove_product,
                Then_throw_product_does_not_exist_exception
                );
        }

        [Scenario]
        public void Update_product()
        {
            Runner.RunScenario(
                Give_admin,
                Give_name,
                Give_cost,
                Give_category,
                Give_existing_product_id,
                When_update_product
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_is_null()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_null_name,
                When_update_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_is_empty()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_empty_name,
                When_update_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_user_is_not_moderator()
        {
            Runner.RunScenario(
                Give_admin,
                When_update_product,
                Then_throw_user_is_not_moderator_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_exist()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_existing_name,
                Give_cost,
                Give_category,
                When_update_product,
                Then_throw_product_name_must_be_unique_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_cost_is_lower_then_zero()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost_lower_than_zero,
                When_update_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_cost_is_equal_zero()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost_equal_zero,
                When_update_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_product_not_exist()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost,
                Give_category,
                Give_not_existing_product_id,
                When_update_product,
                Then_throw_product_does_not_exist_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_empty()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost,
                Give_empty_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_null()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost,
                Give_null_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_wrong()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost,
                Give_incorrect_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_products_cost_in_category_is_greater_one_hundred()
        {
            Runner.RunScenario(
                Give_moderator,
                Give_name,
                Give_cost_greater_than_hundred,
                Give_category,
                Give_existing_product_id,
                When_update_product,
                Then_throw_cost_of_products_in_category_exception
                );
        }
    }
}
