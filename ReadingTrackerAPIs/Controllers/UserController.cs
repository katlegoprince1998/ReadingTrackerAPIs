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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty.");

            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto dto)
        {
            if (dto == null)
                return BadRequest("User data cannot be null.");

            var createdUser = await _userService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty.");

            var deletedUser = await _userService.DeleteAsync(id);

            if (deletedUser == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(deletedUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UserUpdateDto dto)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty.");
            if (dto == null)
                return BadRequest("User update data cannot be null.");

            var updatedUser = await _userService.UpdateAsync(dto, id);

            if (updatedUser == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(updatedUser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }
    }
}
