using SurveyPortalAPI.Data;
using SurveyPortalAPI.Models;

namespace SurveyPortalAPI.Repositories
{
    public class OptionRepository
    {
        private readonly AppDbContext _context;

        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return _context.Options.ToList();
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await _context.Options.FindAsync(id);
        }

        public async Task AddAsync(Option option)
        {
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
        }

        public void Update(Option option)
        {
            _context.Options.Update(option);
            _context.SaveChanges();
        }

        public void Delete(Option option)
        {
            _context.Options.Remove(option);
            _context.SaveChanges();
        }
    }
}
