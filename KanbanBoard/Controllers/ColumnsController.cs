using KanbanBoard.Data;
using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly KanbanContext _context;

        public ColumnsController(KanbanContext context) // create database instance
        {
            _context = context;
        }

        [HttpPost] // Creating Columns
        public async Task<ActionResult<Column>> CreateColumn(Column column)
        {
            if (!await _context.Boards.AnyAsync(c => c.Id == column.BoardId))
            {
                return BadRequest("Invalid Board ID");
            }

            // Count tasks in the same column and await the async call
            var order = await _context.Columns.CountAsync(t => t.BoardId == column.BoardId);

            column.Order = order;

            _context.Columns.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColumn), new { id = column.Id }, column);
        }

        [HttpGet("{id}")] // Get column by id
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }
            return Ok(column);
        }

        [HttpPut("{id}")] // Edit column
        public async Task<IActionResult> EditColumn(int id, Column column)
        {
            if (id != column.Id)
            {
                return BadRequest();
            }

            var columnFromDb = await _context.Columns.FindAsync(id); // fetch column from db

            if (columnFromDb == null) // check if exists
            {
                return NotFound();
            }

            columnFromDb.Name = column.Name; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Columns.Any(e => e.Id == id))
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

        [HttpDelete("{id}")] // Delete Columns
        public async Task<IActionResult> DeleteColumn(int id)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
