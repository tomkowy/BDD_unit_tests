using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class CostOfProductsInCategoryException : Exception
    {
        public CostOfProductsInCategoryException() : base("Cost of products in category exception")
        {
        }
    }
}
