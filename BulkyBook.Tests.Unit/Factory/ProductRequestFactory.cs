using BulkyBookWeb.Api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Tests.Unit.Factory
{
    public class ProductRequestFactory
    {
        public static ProductRequest CreateNewValidProductRequest()
        {
            return new ProductRequest()
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

        public static ProductRequest CreateNewInvalidProductRequest()
        {
            return new ProductRequest()
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
