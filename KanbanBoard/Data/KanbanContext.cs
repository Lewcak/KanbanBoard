using Microsoft.EntityFrameworkCore;
using KanbanBoard.Models;

namespace KanbanBoard.Data
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options)
        {

        }
        public DbSet<TaskItem> Items { get; set; }
    }
}
