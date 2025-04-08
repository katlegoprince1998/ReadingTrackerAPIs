using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Services.Users;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;


       public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id cannot be null or empty");

            var user = await userService.GetAsync(id);

            if (user == null) return NotFound();

            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            if(dto == null) throw new ArgumentNullException(nameof(dto), " cannot be null");

            var createdUser = await userService.CreateAsync(dto);

            if (createdUser == null) return NotFound();

            return Ok(createdUser);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id), " cannot be empty");

            var deletedUser = await userService.DeleteAsync(id);

            if (deletedUser == null) return NotFound();

            return Ok(deletedUser);
        }

        [HttpPut("id")]
         public async Task<IActionResult> UpdateUser(UserUpdateDto dto, Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException("Id cannot be null or empty");

            if (dto == null) throw new ArgumentNullException("Object cannot be null");

            var updatedUser = await userService.UpdateAsync(dto, id);

            if (updatedUser == null) return NotFound();

            return Ok(updatedUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await userService.GetAllUsersAsync();

            if (users == null) return NotFound();

            return Ok(users);
        }
    }
}
