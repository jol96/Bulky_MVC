using BulkyBook.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface IProductsService
    {
        void UpsertProduct(Product product);
        Product GetProduct(int id);
        List<Product> GetProducts();
        (bool isSuccess, string message) DeleteProduct(int? id);
    }
}
