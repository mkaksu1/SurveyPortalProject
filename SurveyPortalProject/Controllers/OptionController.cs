using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyPortalAPI.Data;
using SurveyPortalAPI.Models;

namespace SurveyPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OptionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Option
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptions()
        {
            return await _context.Options.ToListAsync();
        }

        // GET: api/Option/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Option>> GetOption(int id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound();
            }

            return option;
        }

        // POST: api/Option
        [HttpPost]
        public async Task<ActionResult<Option>> CreateOption(Option option)
        {
            _context.Options.Add(option);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOption), new { id = option.Id }, option);
        }

        // PUT: api/Option/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOption(int id, Option option)
        {
            if (id != option.Id)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionExists(id))
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

        // DELETE: api/Option/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OptionExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
