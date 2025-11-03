using Microsoft.EntityFrameworkCore;
using KanbanBoard.Models;

namespace KanbanBoard.Data
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options)
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // for delting entire boards 
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Board>().HasMany(b => b.Columns).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Column>().HasMany(b => b.Tasks).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
