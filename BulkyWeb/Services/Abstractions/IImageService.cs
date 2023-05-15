using BulkyBook.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface IImageService
    {
        void UploadImage(Product product, List<IFormFile>? files);
        void RemoveImage(int? id);
    }
}
