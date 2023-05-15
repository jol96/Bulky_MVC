using BulkyBook.Models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Api
{
    public class UsersApiController: Controller
    {
        private readonly IUsersService _usersService;

        public UsersApiController(IUsersService usersService) 
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        [Route("api/usersapi/getall")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            List<ApplicationUser> userList = _usersService.GetAllUsers();
            return Ok(Json(new { data = userList }));
        }

        /// <summary>
        /// Lock/Unlock a user
        /// </summary>
        /// <param name="id">The user id</param>
        [HttpPost]
        [Route("api/usersapi/lockunlock")]
        [Produces("application/json")]
        public IActionResult LockUnlock(string id)
        {
            var (isSuccess, message) = _usersService.LockUnlockUser(id);
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
