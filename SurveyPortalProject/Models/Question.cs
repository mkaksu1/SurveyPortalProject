namespace SurveyPortalAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
