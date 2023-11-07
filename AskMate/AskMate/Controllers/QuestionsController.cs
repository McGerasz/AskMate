using AskMate.Model;
using AskMate.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AskMate.Controllers;
[ApiController]
[Route("/api/Question")]
public class QuestionsController : ControllerBase
{
    private readonly string _connectionString = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build()
        .GetConnectionString("Default");
    [HttpGet]
    public IActionResult GetAll()
    {
        var repository = new QuestionsRepository(new NpgsqlConnection(_connectionString));

        return Ok(repository.GetAll());
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var repository = new QuestionsRepository(new NpgsqlConnection(_connectionString));

        return Ok(repository.GetById(id));
    }
    [Authorize]
    [HttpPost()]
    public IActionResult Create(Question question)
    {
        var repository = new QuestionsRepository(new NpgsqlConnection(_connectionString));

        return Ok(repository.Create(question));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var answerRepository = new AnswerRepository(new NpgsqlConnection(_connectionString));
        answerRepository.DeleteByQuestionId(id);
        var repository = new QuestionsRepository(new NpgsqlConnection((_connectionString)));
        repository.Delete(id);
        return Ok();
    }
}