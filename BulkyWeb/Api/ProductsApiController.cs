using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Services;
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
                return Ok();
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
            var objProductList = _productsService.GetProducts();
            return Ok(Json(new { data = objProductList }));
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
                return Ok(Json(new { data = product }));
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
