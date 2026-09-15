namespace ProjectManagementApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
