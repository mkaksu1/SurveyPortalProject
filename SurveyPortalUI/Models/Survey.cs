namespace SurveyPortal.MVC.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
