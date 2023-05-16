using BulkyBook.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface IOrderService
    {
        List<OrderHeader> GetOrders(string status, List<OrderHeader> objOrderHeaders);
    }
}
