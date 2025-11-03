using KanbanBoard.Data;
using KanbanBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KanbanController : Controller
    {
        private readonly KanbanContext _context;

        public KanbanController(KanbanContext context) // create database instance
        {
            _context = context;
        }


        // ---- BOARDS ----

        [HttpGet] // get all boards and send to a list
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            return await _context.Boards.ToListAsync();
        }

        [HttpGet("{id}")] // get single board by ID
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }


        //  --- TASKS ----

        [HttpPost] // Creating Tasks 
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
        {
            _context.Items.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TaskItem), new { id = taskItem.Id }, taskItem);
        }

        [HttpDelete] // Deleting Tasks
        public async Task<ActionResult<TaskItem>> DeleteTask(int Id)
        {
            var Task = await _context.Items.FindAsync(Id);
            if (Task == null)
            {
                return NotFound();
            }
            _context.Items.Remove(Task);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // ----- Columns -----


    }
}
