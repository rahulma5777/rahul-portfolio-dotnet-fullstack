using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFullStack.API.Data;
using NetFullStack.API.Models;

namespace NetFullStack.API.Controllers
{
    /// <summary>
    /// Provides CRUD operations for task items.  Tasks belong to users via
    /// a foreign key relationship.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/taskitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            var tasks = await _context.TaskItems
                .Include(t => t.User)
                .ToListAsync();
            return Ok(tasks);
        }

        // GET: api/taskitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var task = await _context.TaskItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/taskitems
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTaskItem([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { id = task.Id }, task);
        }

        // PUT: api/taskitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] TaskItem task)
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

        // DELETE: api/taskitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
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
    }
}