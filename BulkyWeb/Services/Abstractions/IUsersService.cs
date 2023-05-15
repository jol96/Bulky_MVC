using BulkyBook.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface IUsersService
    {
        List<ApplicationUser> GetAllUsers();
        (bool isSuccess, string message) LockUnlockUser(string id);
    }
}
