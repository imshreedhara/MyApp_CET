using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class CETQuestionRepository : ICETQuestionRepository
    {
        private readonly string _connectionString;

        public CETQuestionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CETQuestion>> GetCETQuestionsAsync(int examYear, string subject)
        {
            var questions = new List<CETQuestion>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_GetCETQuestions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters explicitly for better type handling
                    cmd.Parameters.Add(new SqlParameter("@ExamYear", SqlDbType.Int) { Value = examYear });
                    cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject });

                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            questions.Add(new CETQuestion
                            {
                                SerialNumber = reader.GetInt32(reader.GetOrdinal("SerialNumber")),
                                QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                ExamYear = reader.GetInt32(reader.GetOrdinal("ExamYear")),
                                Subject = reader.IsDBNull(reader.GetOrdinal("Subject")) ? string.Empty : reader.GetString(reader.GetOrdinal("Subject")),
                                QuestionText = reader.IsDBNull(reader.GetOrdinal("QuestionText")) ? string.Empty : reader.GetString(reader.GetOrdinal("QuestionText")),
                                OptionA = reader.IsDBNull(reader.GetOrdinal("OptionA")) ? string.Empty : reader.GetString(reader.GetOrdinal("OptionA")),
                                OptionB = reader.IsDBNull(reader.GetOrdinal("OptionB")) ? string.Empty : reader.GetString(reader.GetOrdinal("OptionB")),
                                OptionC = reader.IsDBNull(reader.GetOrdinal("OptionC")) ? string.Empty : reader.GetString(reader.GetOrdinal("OptionC")),
                                OptionD = reader.IsDBNull(reader.GetOrdinal("OptionD")) ? string.Empty : reader.GetString(reader.GetOrdinal("OptionD")),
                                CorrectOption = reader.IsDBNull(reader.GetOrdinal("CorrectOption")) ? string.Empty : reader.GetString(reader.GetOrdinal("CorrectOption")),
                                DiagramPath = reader.IsDBNull(reader.GetOrdinal("DiagramPath")) ? null : reader.GetString(reader.GetOrdinal("DiagramPath")),
                                Comments = reader.IsDBNull(reader.GetOrdinal("Comments")) ? null : reader.GetString(reader.GetOrdinal("Comments")),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a proper logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error fetching CET questions: {ex.Message}");
                throw;
            }

            return questions;
        }
    }
}
