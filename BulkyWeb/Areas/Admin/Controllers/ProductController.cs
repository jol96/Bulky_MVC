using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductsService _productsService;
        private readonly IImageService _imageService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, 
            IProductsService productsService, IImageService imageService, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _productsService = productsService;
            _imageService = imageService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var (objProductList, result) = _unitOfWork.Product.GetAll(includeProperties: "Category");

            if (!result.IsSuccess)
            {
                _logger.LogInformation($"Error occured retreiving products for ProductController Index view." +
                    $"{result.Error?.ErrorCode} \n {result.Error?.ErrorMessage}");
                return View("ErrorView");
            }
            else
            {
                return View(objProductList);
            }         
        }

        public IActionResult Upsert(int? id)
        {
            var (categoryList, result) = _unitOfWork.Category.GetAll();

            if (!result.IsSuccess)
            {
                _logger.LogError($"Error occured retreiving categories for ProductController Upsert view. " +
                    $"{result.Error?.ErrorCode}. {result.Error?.ErrorMessage}");
                return View("ErrorView");
            }

            ProductVM productVM = new()
            {
                CategoryList = categoryList.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new BulkyBook.Models.Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                var productToUpdate = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "ProductImages");
                productVM.Product = productToUpdate;
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                var upsertResult = _productsService.UpsertProduct(productVM.Product);
                if(upsertResult.IsSuccess)
                {
                    _imageService.UploadImage(productVM.Product, files);
                }

                TempData["success"] = "Product created/update successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Item1.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var objProductList = _productsService.GetProducts();
            return Json(new { data = objProductList }); 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var (isSuccess, message) = _productsService.DeleteProduct(id);

            return Json(new { success = isSuccess, message = message });         
        }
        #endregion
    }
}
