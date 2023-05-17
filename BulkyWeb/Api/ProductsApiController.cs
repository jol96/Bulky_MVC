using BulkyBook.Models;
using BulkyBookWeb.Api.models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Api
{
    [ApiController]
    public class ProductsApiController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsApiController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Create/Update a product
        /// </summary>
        [HttpPost]
        [Route("api/productsapi/upsert")]
        [Produces("application/json")]
        public IActionResult Upsert(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                _productsService.UpsertProduct(product);

                ProductResponse upsertProductResponse = new() { 
                  Id = product.Id,
                  Title = product.Title,
                  Description = product.Description,
                  ISBN = product.ISBN,
                  Author = product.Author,
                  ListPrice = product.ListPrice,
                  Price = product.Price,
                  Price50 = product.Price50,
                  Price100 = product.Price100,
                  CategoryId = product.CategoryId,
                  ProductImages = product.ProductImages
                };

                return Ok(Json(new { data = upsertProductResponse}));
            }
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        [HttpGet]
        [Route("api/productsapi/getall")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var productList = _productsService.GetProducts();
            List<ProductResponse> productResponseList = new List<ProductResponse>();
            foreach (var product in productList)
            {
                ProductResponse productResponse = new()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    ISBN = product.ISBN,
                    Author = product.Author,
                    ListPrice = product.ListPrice,
                    Price = product.Price,
                    Price50 = product.Price50,
                    Price100 = product.Price100,
                    CategoryId = product.CategoryId,
                    ProductImages = product.ProductImages
                };
                productResponseList.Add(productResponse);
            }
            return Ok(Json(new { data = productResponseList }));
        }

        /// <summary>
        /// Get a single product
        /// </summary>
        /// <param name="id">The id of the product</param>
        [HttpGet]
        [Route("api/productsapi/get")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var product = _productsService.GetProduct(id);
            if (product == null)
            {
                return BadRequest($"No product found with id {id}");
            }
            else 
            {
                ProductResponse productResponse = new()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    ISBN = product.ISBN,
                    Author = product.Author,
                    ListPrice = product.ListPrice,
                    Price = product.Price,
                    Price50 = product.Price50,
                    Price100 = product.Price100,
                    CategoryId = product.CategoryId,
                    ProductImages = product.ProductImages
                };
                return Ok(Json(new { data = productResponse }));
            }          
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        [HttpDelete]
        [Route("api/productsapi/delete")]
        [Produces("application/json")]
        public IActionResult Delete(int? id)
        {
            var (isSuccess, message) = _productsService.DeleteProduct(id);

            if (!isSuccess)
            {
                return StatusCode(500, $"Internal Server Error. {message}");
            }
            else 
            {
                return Ok(200);
            }
        }
    }
}
