using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class ProductNameCannotBeEmptyException : Exception
    {
        public ProductNameCannotBeEmptyException() : base("Product name cannot be empty")
        {
        }
    }
}
