using KanbanBoard.Data;
using KanbanBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {

        private readonly KanbanContext _context;

        public BoardsController(KanbanContext context) // create database instance
        {
            _context = context;
        }

        [HttpGet] // get all boards and send to a list
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            return await _context.Boards.ToListAsync();
        }

        [HttpGet("{id}")] // get board by ID, with all its columns and tasks 
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            var board = await _context.Boards.Include(c => c.Columns).ThenInclude(t => t.Tasks).FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        [HttpPost] // Create new Board
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {

            board.Columns = new List<Column>
            {
                new Column { Name = "To Do", Order = 0 },
                new Column { Name = "In Progress", Order = 1 },
                new Column { Name = "Done", Order = 2 }
            };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        [HttpPut("{id}")] // Edit Board
        public async Task<IActionResult> EditBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            var boardFromDb = await _context.Boards.FindAsync(id);

            if (boardFromDb == null)
            {
                return NotFound();
            }

            boardFromDb.Name = board.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")] // Delete Board
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

