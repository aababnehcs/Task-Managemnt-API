using System;
using TaskManagementAPI.Dtos;
using TaskManagementAPI.Models;

	namespace TaskManagementAPI.Services
	{
		public interface ITaskService
		{
        Task<TaskItem> CreateTask(TaskCreateDto taskDto);
        Task<TaskItem> GetTaskById(int id, int currentUserId, bool isAdmin);
        Task<bool> UpdateTask(int id, TaskUpdateDto taskDto, int currentUserId, bool isAdmin);
        Task<bool> DeleteTask(int id);
        Task<IEnumerable<TaskItem>> GetAllTasks();
    }
	}


