using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface ICETQuestionRepository
    {
        Task<IEnumerable<CETQuestion>> GetCETQuestionsAsync(int examYear, string subject);
    }
}
