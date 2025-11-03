namespace KanbanBoard.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order {  get; set; }
        public List<TaskItem> Tasks { get; set; }

        public int BoardId { get; set; }
    }
}
