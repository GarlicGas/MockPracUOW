using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockPracUOW.Server.Data;
using MockPracUOW.Server.IRepository;
using MockPracUOW.Shared.Domain;

//IMPORTANT NOTE: If the entity has a foreign key, in the GetTasks() [MIND THE PLURAL, IT IS IMPORTANT THAT THE FUNCTION NAME HAS A PLURAL!] function, do .GetAll(includes: q => q.Include(x=> x.FkEntity))
//you can add more .Include in the case of more foregin keys. For example: await _unitOfWork.Bookings.GetAll(includes: q => q.Include(x=> x.Vehicle).Include(x=>x.Customer));

namespace MockPracUOW.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _unitOfWork.Tasks.GetAll(); //in case you dont get what I mean by .GetAll, I meant THIS .GetAll
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _unitOfWork.Tasks.Get(q => q.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Shared.Domain.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            //_context.Entry(task).State = EntityState.Modified;
            _unitOfWork.Tasks.Update(task);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shared.Domain.Task>> PostTask(Shared.Domain.Task task)
        {
            //_context.Tasks.Add(task);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Tasks.Insert(task);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            //var task = await _context.Tasks.FindAsync(id);
            var task = await _unitOfWork.Tasks.Get(q => q.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            //_context.Tasks.Remove(task);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Tasks.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> TaskExists(int id)
        {
            //return _context.Tasks.Any(e => e.Id == id);
            var task = await _unitOfWork.Tasks.Get(q => q.Id == id);
            return task != null;
        }
    }
}
