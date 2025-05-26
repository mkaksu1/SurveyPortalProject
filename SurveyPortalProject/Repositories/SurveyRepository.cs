using SurveyPortalAPI.Data;
using SurveyPortalAPI.Models;

namespace SurveyPortalAPI.Repositories
{
    public class SurveyRepository
    {
        private readonly AppDbContext _context;

        public SurveyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Survey>> GetAllAsync()
        {
            return _context.Surveys.ToList();
        }

        public async Task<Survey> GetByIdAsync(int id)
        {
            return await _context.Surveys.FindAsync(id);
        }

        public async Task AddAsync(Survey survey)
        {
            await _context.Surveys.AddAsync(survey);
            await _context.SaveChangesAsync();
        }

        public void Update(Survey survey)
        {
            _context.Surveys.Update(survey);
            _context.SaveChanges();
        }

        public void Delete(Survey survey)
        {
            _context.Surveys.Remove(survey);
            _context.SaveChanges();
        }
    }
}
