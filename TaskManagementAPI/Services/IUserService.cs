using System;
using TaskManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.Dtos;

namespace TaskManagementAPI.Services
{




    public interface IUserService
    {
        Task<User> CreateUser(UserCreateDto userDto);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UpdateUser(int id, UserUpdateDto userDto);
        Task<bool> DeleteUser(int id);
        Task<User> Authenticate(string username, string password);
    }
	}


