using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Services;
using MyApp.Domain.Entities;

namespace MyApp.Controllers
{
    [Route("api/CETQuestions")]
    [ApiController]
    public class CETQuestionsController : ControllerBase
    {
        private readonly CETQuestionService _service;
        private readonly ILogger<CETQuestionsController> _logger;

        public CETQuestionsController(CETQuestionService service, ILogger<CETQuestionsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{examYear}/{subject}")]
        public async Task<IActionResult> GetQuestions(int examYear, string subject)
        {
            IEnumerable<CETQuestion>? questions = null;

            try
            {
                _logger.LogInformation($"Fetching CET questions for Year: {examYear}, Subject: {subject}");
                questions = await _service.GetQuestionsAsync(examYear, subject);

                if (questions == null || !questions.Any())
                {
                    _logger.LogWarning($"No questions found for Year: {examYear}, Subject: {subject}");
                    return NotFound(new { Message = "No questions found." });
                }

                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CET questions");
                return StatusCode(500, new { Message = "An error occurred while fetching questions." });
            }
            finally
            {
                _logger.LogInformation($"Completed request for Year: {examYear}, Subject: {subject}");
            }
        }
    }
}
