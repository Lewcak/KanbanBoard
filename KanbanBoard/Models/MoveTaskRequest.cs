namespace KanbanBoard.Models
{
    public class MoveTaskRequest
    {
        public int TaskId { get; set; }
        public int NewColumnId { get; set; }
        public int NewOrderIndex { get; set; }
    }
}
