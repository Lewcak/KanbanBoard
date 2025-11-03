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

        public KanbanController(KanbanContext context)
        {
            _context = context;
        }



    }
}
