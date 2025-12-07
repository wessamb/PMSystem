namespace PMSystem.Application.DTOs.TaskItems
{
    public class UpdateTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }
    }
}
