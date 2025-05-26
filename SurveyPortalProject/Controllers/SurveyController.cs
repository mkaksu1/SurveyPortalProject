using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyPortalAPI.Data;
using SurveyPortalAPI.Models;

namespace SurveyPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm işlemler sadece giriş yapmış kullanıcılar için geçerli
    public class SurveyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SurveyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Survey
        [HttpGet]
        [AllowAnonymous] // Anket listesini herkes görebilir
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
            return await _context.Surveys.Include(s => s.Questions).ThenInclude(q => q.Options).ToListAsync();
        }

        // GET: api/Survey/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // POST: api/Survey
        [HttpPost]
        public async Task<ActionResult<Survey>> CreateSurvey(Survey survey)
        {
            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSurvey), new { id = survey.Id }, survey);
        }

        // PUT: api/Survey/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurvey(int id, Survey survey)
        {
            if (id != survey.Id)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // DELETE: api/Survey/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.Id == id);
        }
    }
}
