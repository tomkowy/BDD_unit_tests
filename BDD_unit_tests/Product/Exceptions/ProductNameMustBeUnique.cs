using System;

namespace BDD_unit_tests.Product.Exceptions
{
    public class ProductNameMustBeUnique : Exception
    {
        public ProductNameMustBeUnique() : base("Product name must be unique")
        {
        }
    }
}
