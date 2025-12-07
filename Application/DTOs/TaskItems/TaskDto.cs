namespace PMSystem.Application.DTOs.TaskItems
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public Guid ProjectId { get; set; }

        public Guid AssignedUserId { get; set; }


    }
}
