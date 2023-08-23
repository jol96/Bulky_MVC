using BulkyBook.Models;
using BulkyBookWeb.Repository.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface IProductsService
    {
        OperationResult UpsertProduct(Product product);
        Product GetProduct(int id);
        List<Product> GetProducts();
        (bool isSuccess, string message) DeleteProduct(int? id);
    }
}
