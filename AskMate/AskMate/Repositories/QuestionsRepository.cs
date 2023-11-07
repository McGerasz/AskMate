using System.Data;
using System.Security.Claims;
using AskMate.Controllers;
using AskMate.Model;
using Npgsql;

namespace AskMate.Repositories;

public class QuestionsRepository
{
    private readonly NpgsqlConnection _connection;

    public QuestionsRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }   
    public List<Question> GetAll()
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter("SELECT * FROM questions", _connection);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var table = dataSet.Tables[0];

        var queryResult = new List<Question>();
        foreach (DataRow row in table.Rows)
        {
            queryResult.Add(new Question
            {
                Id = (int)row["id"],
                Title = (string)row["title"],
                Description = (string)row["description"],
                Submission_time = (DateTime)row["submission_time"]
            });
        }

        _connection.Close();
        queryResult = new List<Question>(queryResult.OrderByDescending(q => q.Submission_time));
        return queryResult;
    }
    public Question GetById(int id)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter("SELECT * FROM questions WHERE id = :id", _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var table = dataSet.Tables[0];

        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            return new Question
            {
                Id = (int)row["id"],
                Title = (string)row["title"],
                Description = (string)row["description"],
                Submission_time = (DateTime)row["submission_time"]
            };
        }

        _connection.Close();

        return null;
    }
    public int Create(Question question)
    {
        
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "INSERT INTO questions (title, description, submission_time) VALUES " +
            "(:title, :description, :submission_time) RETURNING id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":title", question.Title);
        adapter.SelectCommand?.Parameters.AddWithValue(":description", question.Description);
        adapter.SelectCommand?.Parameters.AddWithValue(":submission_time", question.Submission_time);
        var lastInsertId = (int)adapter.SelectCommand?.ExecuteScalar();
        _connection.Close();

        return lastInsertId;
    }
    public void Delete(int id)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "DELETE FROM questions WHERE id = :id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);

        adapter.SelectCommand?.ExecuteNonQuery();
        _connection.Close();
    }
}