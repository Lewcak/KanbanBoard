namespace KanbanBoard.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }

        public int ColumnId { get; set; }
    }
}
