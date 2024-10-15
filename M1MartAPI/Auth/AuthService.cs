using M1MartAPI.Auth.AuthDtos;
using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace M1MartAPI.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string Register(RegisterRequestDto dto)
        {
            try
            {
                bool isUserExist = _userRepository.CheckUserIsExist(dto.Username);
                if (isUserExist) throw new Exception($"Sorry username {dto.Username} is already registered");

                var user = new User()
                {
                    Username = dto.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = dto.Role,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                };
                var userRegistered = _userRepository.Add(user);
                return userRegistered.Username;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginResponseDto Login(LoginRequestDto dto)
        {
            try
            {
                var user = _userRepository.GetByUsername(dto.Username);
                bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
                if (isCorrectPassword) return new LoginResponseDto()
                {
                    Username = dto.Username,
                    Token = CreateToken(user),
                    Role = user.Role,
                };
                
                throw new Exception("Username or Password Incorrect");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginResponseDto GetCurrentUser(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return new LoginResponseDto()
            {
                Username = username,
                Token = CreateToken(user),
                Role = user.Role.ToString(),
            };
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>(){
                //new Claim(ClaimTypes.NameIdentifier, user.Username),
                //new Claim(ClaimTypes.Role, user.Role)
                new Claim("username", user.Username),
                new Claim("role", user.Role),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
