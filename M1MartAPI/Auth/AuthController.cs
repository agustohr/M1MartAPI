using M1MartAPI.Auth.AuthDtos;
using M1MartAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace M1MartAPI.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                string usernameRegistered = _authService.Register(dto);
                return Ok(new ResponseDto<string>()
                {
                    Status = "SUCCESS",
                    Message = $"User with username {dto.Username} is succesfully registered",
                    Data = usernameRegistered
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var loginResponse = _authService.Login(dto);
                return Ok(new ResponseDto<LoginResponseDto>()
                {
                    Status = "SUCCESS",
                    Message = "This user is successfully authenticated",
                    Data = loginResponse
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

        //[Authorize]
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            try {
                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(currentUser == null) return NotFound();
                var user = _authService.GetCurrentUser(currentUser);
                if (user == null) return NotFound();
                
                return Ok(user);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
