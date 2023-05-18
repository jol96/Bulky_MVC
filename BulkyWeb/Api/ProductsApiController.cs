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
        public IActionResult Upsert(ProductRequest productrequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Product product = new() {
                    Title = productrequest.Title,
                    Description = productrequest.Description,
                    ISBN = productrequest.ISBN,
                    Author = productrequest.Author,
                    ListPrice = productrequest.ListPrice,
                    Price = productrequest.Price,
                    Price50 = productrequest.Price50,
                    Price100 = productrequest.Price100,
                    CategoryId = productrequest.CategoryId,
                };

                _productsService.UpsertProduct(product);

                return Ok(product);
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
            return Ok(productList);
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
                return Ok(product);
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
