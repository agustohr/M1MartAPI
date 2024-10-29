using M1MartAPI.Users.UserDtos;
using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;

namespace M1MartAPI.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll().Select(u => new UserDto()
            {
                Username = u.Username,
                Role = u.Role,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            });
            return users.ToList();
        }

        public UserDto GetByUsername(string username)
        {
            try {
                var user = _userRepository.GetByUsername(username);
                return new UserDto()
                {
                    Username = user.Username,
                    Role = user.Role,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserDto CreateUser(UserDto dto)
        {
            var user = new User()
            {
                Username = dto.Username,
                //Password = dto.Password,
                Role = dto.Role,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            };
            var createdUser = _userRepository.Add(user);
            return new UserDto()
            {
                Username = createdUser.Username,
                //Password = createdUser.Password,
                Role = createdUser.Role,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
            };
            //return _mapper.Map<UserDto>(createdUser);
        }

        public UserDto UpdateUser(string username, UpdateUserDto dto)
        {
            try {
                var user = _userRepository.GetByUsername(username);
                user.Password = dto.Password;
                user.Role = dto.Role;

                var updatedUser = _userRepository.Update(user);
                return new UserDto()
                {
                    Username = user.Username,
                    //Password = user.Password,
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteUser(string username)
        {
            try {
                return _userRepository.Delete(username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
