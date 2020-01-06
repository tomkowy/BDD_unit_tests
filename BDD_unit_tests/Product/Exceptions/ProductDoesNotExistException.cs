using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException() : base("Product does not exist")
        {
        }
    }
}
