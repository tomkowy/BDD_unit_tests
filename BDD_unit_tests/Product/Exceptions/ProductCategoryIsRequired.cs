using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class ProductCategoryIsRequired : Exception
    {
        public ProductCategoryIsRequired() : base("Product category is required")
        {
        }
    }
}
