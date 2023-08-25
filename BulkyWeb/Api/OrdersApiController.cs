using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Api
{
    [ApiController]
    public class OrdersApiController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersApiController(IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <param name="status"></param>
        /// <returns>A list of orders based on the status parameter</returns>
        [HttpGet]
        [Route("api/ordersapi/getall")]
        [Produces("application/json")]
        public IActionResult GetAll(string status)

        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                var (orderList, result) = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
                objOrderHeaders = orderList;
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var (orderList, result) = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
                objOrderHeaders = orderList;
            }

            objOrderHeaders = _orderService.GetOrders(status, (List<OrderHeader>)objOrderHeaders);

            return Ok(Json(new { data = objOrderHeaders }));
        }
    }
}
