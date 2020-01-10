using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class NumberOfProductsInCategoryException : Exception
    {
        public NumberOfProductsInCategoryException() : base("Number of products in category exception")
        {
        }
    }
}
