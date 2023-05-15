using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using BulkyBook.Models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace BulkyBookWeb.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void UploadImage(Product product, List<IFormFile>? files)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (files != null)
            {
                foreach (var file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = @"images\products\product-" + product.Id;
                    string finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath))
                    {
                        Directory.CreateDirectory(finalPath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    ProductImage productImage = new()
                    {
                        ImageUrl = @"\" + productPath + @"\" + fileName,
                        ProductId = product.Id
                    };

                    if (product.ProductImages == null)
                        product.ProductImages = new List<ProductImage>();

                    product.ProductImages.Add(productImage);
                }

                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
            }
        }

        public void RemoveImage(int? id)
        {
            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
        }
    }
}
