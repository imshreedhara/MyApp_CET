using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class CETQuestionService
    {
        private readonly ICETQuestionRepository _repository;

        public CETQuestionService(ICETQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CETQuestion>> GetQuestionsAsync(int examYear, string subject)
        {
            return await _repository.GetCETQuestionsAsync(examYear, subject);
        }
    }
}
