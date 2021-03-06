﻿using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace BDD_unit_tests.Tests.Product.ComplexTest_BetterSetup
{
    public partial class ProductServiceTest
    {
        [Scenario]
        public void Can_add_product()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_small,
                When_add_product,
                Then_throw_no_exception,
                Then_added_product_id_is_not_empty
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_is_empty()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_empty_product_name,
                When_add_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_is_null()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_null_product_name,
                When_add_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_user_is_not_admin()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                When_add_product,
                Then_throw_user_is_not_admin_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_name_exist()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_existing_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_small,
                When_add_product,
                Then_throw_product_name_must_be_unique_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_cost_is_lower_than_zero()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_lower_than_zero,
                When_add_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_cost_is_equal_zero()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_equal_zero,
                When_add_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_empty()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_empty_product_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_null()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_null_product_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_category_is_incorrect()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_incorrect_product_category,
                When_add_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_products_cost_in_category_is_greater_one_hundred()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_hundred,
                Given_product_category_small,
                When_add_product,
                Then_throw_cost_of_products_in_category_exception
                );
        }

        [Scenario]
        public void Cannot_add_product_when_products_count_in_category_is_greater_than_five()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_big,
                Given_five_products_from_category_big,
                When_add_product,
                Then_throw_number_of_products_in_category_exception
                );
        }

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

        [Scenario]
        public void Cannot_remove_product_when_user_is_not_admin()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                When_remove_product,
                Then_throw_user_is_not_admin_exception
                );
        }

        [Scenario]
        public void Cannot_remove_product_when_product_not_exist()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                Given_not_existing_product_id,
                When_remove_product,
                Then_throw_product_does_not_exist_exception
                );
        }

        [Scenario]
        public void Update_product()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_small,
                Given_existing_product_id,
                When_update_product,
                Then_throw_no_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_is_null()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_null_product_name,
                When_update_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_is_empty()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_empty_product_name,
                When_update_product,
                Then_throw_product_name_cannot_be_empty_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_user_is_not_moderator()
        {
            Runner.RunScenario(
                Given_current_user_as_admin,
                When_update_product,
                Then_throw_user_is_not_moderator_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_name_exist()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_existing_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_small,
                When_update_product,
                Then_throw_product_name_must_be_unique_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_cost_is_lower_then_zero()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_lower_than_zero,
                When_update_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_cost_is_equal_zero()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_equal_zero,
                When_update_product,
                Then_throw_product_cost_must_be_greates_than_zero_exception
                );
        }

        [Scenario]
        public void Cannot_update_product_when_product_not_exist()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_product_category_small,
                Given_not_existing_product_id,
                When_update_product,
                Then_throw_product_does_not_exist_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_empty()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_empty_product_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_null()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_null_product_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_product_category_is_wrong()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_zero,
                Given_incorrect_product_category,
                When_update_product,
                Then_throw_product_category_is_required_exception
                );
        }

        [Scenario]
        public void Throw_exception_when_updating_products_cost_in_category_is_greater_one_hundred()
        {
            Runner.RunScenario(
                Given_current_user_as_moderator,
                Given_unique_product_name,
                Given_product_cost_greater_than_hundred,
                Given_product_category_small,
                Given_existing_product_id,
                When_update_product,
                Then_throw_cost_of_products_in_category_exception
                );
        }
    }
}
