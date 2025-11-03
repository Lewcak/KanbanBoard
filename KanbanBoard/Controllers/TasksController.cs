using KanbanBoard.Data;
using KanbanBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly KanbanContext _context;
        
        public TasksController(KanbanContext context) // create database instance
        {
            _context = context;
        }

        [HttpPost] // Creating Tasks 
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
        {
            if (!await _context.Columns.AnyAsync(c => c.Id == taskItem.ColumnId))
            {
                return BadRequest("Invalid Column ID");
            }

            // Count tasks in the same column and await the async call
            var order = await _context.TaskItems.CountAsync(t => t.ColumnId == taskItem.ColumnId);

            taskItem.Order = order;

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
        }

        [HttpDelete("{id}")] // Deleting Tasks
        public async Task<ActionResult<TaskItem>> DeleteTask(int Id)
        {
            var Task = await _context.TaskItems.FindAsync(Id);
            if (Task == null)
            {
                return NotFound();
            }
            _context.TaskItems.Remove(Task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")] // Get task by id
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{id}")] // Edit task 
        public async Task<IActionResult> EditTask(int id,TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_context.TaskItems.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

    }
}
