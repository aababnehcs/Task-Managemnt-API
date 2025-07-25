using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
	public interface ITaskRepository
	{
		Task<TaskItem> GetById(int id);
		Task<IEnumerable<TaskItem>> GetAll();
		Task<IEnumerable<TaskItem>> GetByAssignedUserId(int userId);
		Task<TaskItem> Add(TaskItem task);
		Task<bool> Update(TaskItem task);
		Task<bool> Delete(int id);
	}
}