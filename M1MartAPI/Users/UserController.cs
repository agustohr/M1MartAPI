using M1MartAPI.Shared;
using M1MartAPI.Users.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Users
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                var res = new ResponseDto<List<UserDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {users.Count()} users.",
                    Data = users
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            try {
                var user = _userService.GetByUsername(username);
                var res = new ResponseDto<UserDto>()
                {
                    Status = "SUCCESS",
                    Message = $"Here you've a single user requested for username {username}.",
                    Data = user
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto dto)
        {
            var user = _userService.CreateUser(dto);

            var res = new ResponseDto<UserDto>()
            {
                Status = "SUCCESS",
                Message = $"User with username {dto.Username} is successfully added, here is the user you sent.",
                Data = user
            };
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, res);
        }

        [HttpPut("{username}")]
        public IActionResult EditUser(string username, [FromBody] UpdateUserDto dto)
        {
            try {

                if (ModelState.IsValid) {
                    var updatedUser = _userService.UpdateUser(username, dto);
                    return Ok(new ResponseDto<UserDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"The user with username {username} has been updated with your provided data.",
                        Data = updatedUser
                    });
                }

                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                return BadRequest(new ResponseDto<string>() {
                    Status = "BAD REQUEST",
                    Message = "Invalid input data.",
                    Errors = errors
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            var deleted = _userService.DeleteUser(username);
            var res = new ResponseDto<string>()
            {
                Status = "SUCCESS",
                Message = $"User with username {username} has been deleted.",
            };
            if (!deleted)
            {
                res.Status = "NOT FOUND";
                res.Message = $"User with username {username} is not found.";
                return NotFound(res);
            }
            return Ok(res);
        }
    }
}
