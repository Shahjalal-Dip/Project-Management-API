namespace ProjectManagementApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
