using TaskManagementAPI.Data;
using TaskManagementAPI.Dtos;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskItem> CreateTask(TaskCreateDto taskDto)
        {
            
            var assignedUser = await _userRepository.GetById(taskDto.AssignedUserId);
            if (assignedUser == null)
            {
                throw new InvalidOperationException("Assigned user not found");
            }

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = TaskItemStatus.Pending,
                AssignedUserId = taskDto.AssignedUserId
            };

            return await _taskRepository.Add(task);
        }

        public async Task<TaskItem> GetTaskById(int id, int currentUserId, bool isAdmin)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return null;
            }

           
            if (!isAdmin && task.AssignedUserId != currentUserId)
            {
                return null;
            }

            return task;
        }

        public async Task<bool> UpdateTask(int id, TaskUpdateDto taskDto, int currentUserId, bool isAdmin)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return false;

           
            if (!isAdmin && task.AssignedUserId != currentUserId)
                return false;

           
            if (!isAdmin && taskDto.Status.Equals != null)
                return false;

         
            if (!string.IsNullOrEmpty(taskDto.Title))
                task.Title = taskDto.Title;

            if (taskDto.Description != null)
                task.Description = taskDto.Description;

           
            if (taskDto.Status.Equals != null)
            {
                task.Status = taskDto.Status;

               

            }

           
            if (isAdmin && taskDto.AssignedUserId.HasValue)
            {
                var assignedUser = await _userRepository.GetById(taskDto.AssignedUserId.Value);
                if (assignedUser == null) throw new InvalidOperationException("Assigned user not found");
                task.AssignedUserId = taskDto.AssignedUserId.Value;
            }

            return await _taskRepository.Update(task);
        }
        public async Task<bool> DeleteTask(int id)
        {
            return await _taskRepository.Delete(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            return await _taskRepository.GetAll();
        }
    }
}