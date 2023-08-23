using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Repository.Models;
using BulkyBookWeb.Services.Abstractions;

namespace BulkyBookWeb.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public ProductsService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public OperationResult UpsertProduct(Product product)
        {
            if (product.Id == 0)
            {
                _unitOfWork.Product.Add(product);
            }
            else
            {
                _unitOfWork.Product.Update(product);
            }

            return _unitOfWork.Save();
        }

        public List<Product> GetProducts()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return objProductList;
        }

        public Product GetProduct(int id)
        {
            var product = _unitOfWork.Product.Get(p=>p.Id == id);
            return product;
        }

        public (bool isSuccess, string message) DeleteProduct(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return (false, "Error deleting product");
            }
            else
            {
                _imageService.RemoveImage(id);
                _unitOfWork.Product.Remove(productToBeDeleted);
                _unitOfWork.Save();
            }
            return (true, "Product deleted successfully");
        }      
    }
}
