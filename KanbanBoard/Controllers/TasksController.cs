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

        [HttpDelete("{id}")] // Delete Tasks
        public async Task<ActionResult<TaskItem>> DeleteTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.TaskItems.Remove(task);
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
        public async Task<IActionResult> EditTask(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var taskFromDb = await _context.TaskItems.FindAsync(id); // fetch task

            if (taskFromDb == null) // check if exists
            {
                return NotFound();
            }

            taskFromDb.Title = task.Title;
            taskFromDb.Description = task.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

        [HttpPut("move")]
        public async Task<IActionResult> MoveTask([FromBody] MoveTaskRequest request)
        {
            var taskToMove = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == request.TaskId); // Get task being moved

            if (taskToMove == null)
            {
                return NotFound("Task Not Found");
            }
            
            var oldColumnId = taskToMove.ColumnId;

            if (oldColumnId == request.NewColumnId) // in the same column
            {
                // get all tasks in order
                var tasksInColumn = await _context.TaskItems.Where(b => b.ColumnId == oldColumnId).OrderBy(b => b.Order).ToListAsync();

                var task = tasksInColumn.First(t => t.Id == request.TaskId); // Remove task from list
                tasksInColumn.Remove(task);

                tasksInColumn.Insert(request.NewOrderIndex, task); // Add task to new position 

                for (int i = 0; i < tasksInColumn.Count; i++)
                {
                    tasksInColumn[i].Order = i;
                }
            }
            else // moving to a diffrent column
            {
                // get all tasks in from old column
                var taskInColumn = await _context.TaskItems.Where(t => t.ColumnId == oldColumnId).OrderBy(t  => t.Order).ToListAsync(); 

                var task = taskInColumn.First(i => i.Id == request.TaskId); // remove task from old column
                taskInColumn.Remove(task);

                for (int i = 0;i < taskInColumn.Count; i++) // reorder old column
                {
                    taskInColumn[i].Order = i;
                }

                // get all tasks in new column
                var tasksInNewColumn = await _context.TaskItems.Where(t => t.ColumnId == request.NewColumnId).OrderBy(t=>t.Order).ToListAsync();

                tasksInNewColumn.Insert(request.NewOrderIndex, task); // add task to new column with requested index

                task.ColumnId = request.NewColumnId; // update task column id

                for (int i = 0; i < tasksInNewColumn.Count; i++) // reorder new column
                {
                    tasksInNewColumn[i].Order = i;
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
