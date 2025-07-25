using System.Security.Claims;
using TaskManagementAPI.Data;
using TaskManagementAPI.Dtos;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUser(UserCreateDto userDto)
        {
            // Check if username exists
            var existingUser = await _userRepository.GetByUsername(userDto.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists");
            }

            // Create new user
            var user = new User
            {
                Username = userDto.Username,
                Password = _passwordHasher.HashPassword(userDto.Password),
                Role = userDto.Role
            };

            return await _userRepository.Add(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<bool> UpdateUser(int id, UserUpdateDto userDto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }

         
            if (!string.IsNullOrEmpty(userDto.Username))
            {
                var existingUser = await _userRepository.GetByUsername(userDto.Username);
                if (existingUser != null && existingUser.Id != id)
                {
                    throw new InvalidOperationException("Username already taken");
                }
                user.Username = userDto.Username;
            }

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = _passwordHasher.HashPassword(userDto.Password);
            }

            if (userDto.Role.HasValue)
            {
                user.Role = userDto.Role.Value;
            }

            return await _userRepository.Update(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.Delete(id);
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user == null || !_passwordHasher.VerifyPassword(password, user.Password))
                return null;

            return user;
        }
    }
}