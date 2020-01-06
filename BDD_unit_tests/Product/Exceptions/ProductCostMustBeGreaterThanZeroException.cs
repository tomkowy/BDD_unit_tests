using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class ProductCostMustBeGreaterThanZeroException : Exception
    {
        public ProductCostMustBeGreaterThanZeroException() : base("Product cost must be greater than zero")
        {
        }
    }
}
