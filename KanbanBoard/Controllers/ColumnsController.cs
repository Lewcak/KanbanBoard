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


        


    }
}
