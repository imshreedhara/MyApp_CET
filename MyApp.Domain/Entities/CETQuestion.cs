namespace MyApp.Domain.Entities
{
    public class CETQuestion
    {
        public int SerialNumber { get; set; }
        public int QuestionID { get; set; }
        public int ExamYear { get; set; }
        public string? Subject { get; set; }
        public string? QuestionText { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? CorrectOption { get; set; }
        public string? DiagramPath { get; set; }
        public string? Comments { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
