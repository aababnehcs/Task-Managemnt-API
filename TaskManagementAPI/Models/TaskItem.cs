using System;
namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public TaskItemStatus Status { get; set; }


    }
}

