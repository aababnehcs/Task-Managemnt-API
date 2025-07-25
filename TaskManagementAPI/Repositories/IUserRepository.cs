using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<IEnumerable<User>> GetAll();
        Task<User> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
    }
}