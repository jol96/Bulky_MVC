using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Tests.Unit.Factory
{
    public class ProductFactory
    {
        public static Product CreateNewValidProduct()
        {
            return new Product()
            {
                Title = "Harry Potter",
                ISBN = "978-3-16-148410-0",
                Author = "J.K",
                ListPrice = 9.00,
                Price = 9.00,
                Price50 = 5.00,
                Price100 = 4.00,
                CategoryId = 1
            };
        }

        public static Product GetExistingValidProduct()
        {
            return new Product()
            {
                Id = 1,
                Title = "Lord of the Rings",
                ISBN = "978-3-16-148410-1",
                Author = "J. R. R. Tolkien",
                ListPrice = 9.00,
                Price = 9.00,
                Price50 = 5.00,
                Price100 = 4.00,
                CategoryId = 1
            };
        }

        public static Product CreateNewInvalidProduct()
        {
            return new Product()
            {
                Title = "Harry Potter",
                ISBN = "978-3-16-148410-0",
                Author = "J.K",
                ListPrice = 9.00,
                Price = 9.00,
                Price50 = 5.00,
                Price100 = 4.00,
                CategoryId = 0
            };
        }
    }
}
