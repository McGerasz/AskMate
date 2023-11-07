using AskMate.Model;
using AskMate.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AskMate.Controllers;
[ApiController]
[Route("/api/Answer")]
public class AnswerController : ControllerBase
{
    private readonly string _connectionString = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build()
        .GetConnectionString("Default");
    
    [Authorize]
    [HttpPost()]
    public IActionResult Create(Answer answer)
    {
        try
        {
        var repository = new AnswerRepository(new NpgsqlConnection(_connectionString));

        return Ok(repository.Create(answer));

        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    [HttpDelete("/api/Answer/question/{question_id}")]
    public IActionResult DeleteByQuestionId(int question_id)
    {
        var repository = new AnswerRepository(new NpgsqlConnection((_connectionString)));
        repository.DeleteByQuestionId(question_id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var repository = new AnswerRepository(new NpgsqlConnection(_connectionString));
        repository.Delete(id);
        return Ok();
    }
}