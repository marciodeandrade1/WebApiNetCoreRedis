using Microsoft.AspNetCore.Mvc;
using WebApiNetCoreRedis.Core.Entities;
using WebApiNetCoreRedis.Infra.Persistence;
using WebApiNetCoreRedis.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiNetCoreRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoListDbContext _context;
        public ToDosController(ToDoListDbContext context)
        {
            _context = context;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            List<ToDo> todo = await _context.ToDos.ToListAsync();
            if (todo == null || todo.Count == 0)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ToDo? todo = await _context.ToDos.SingleOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoInputModel model)
        {
            var todo = new ToDo(0, model.Title, model.Description);

            await _context.ToDos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, model);
        }
    }
}
