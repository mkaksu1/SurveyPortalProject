using SurveyPortalAPI.Data;
using SurveyPortalAPI.Models;

namespace SurveyPortalAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllAsync()
        {
            return _context.Questions.ToList();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public void Update(Question question)
        {
            _context.Questions.Update(question);
            _context.SaveChanges();
        }

        public void Delete(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }
    }
}
