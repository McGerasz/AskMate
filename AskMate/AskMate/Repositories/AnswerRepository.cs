using AskMate.Model;
using Npgsql;

namespace AskMate.Repositories;

public class AnswerRepository
{
    private readonly NpgsqlConnection _connection;

    public AnswerRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }   
    public int Create(Answer answer)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "INSERT INTO answers (message, question_id, submission_time) VALUES (:message, :question_id, :submission_time) RETURNING id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":message", answer.Message);
        adapter.SelectCommand?.Parameters.AddWithValue(":question_id", answer.Question_id);
        adapter.SelectCommand?.Parameters.AddWithValue(":submission_time", answer.Submission_time);

        var lastInsertId = (int)adapter.SelectCommand?.ExecuteScalar();
        _connection.Close();

        return lastInsertId;
    }

    public void DeleteByQuestionId(int question_id)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "DELETE FROM answers WHERE question_id = :question_id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":question_id", question_id);

        adapter.SelectCommand?.ExecuteNonQuery();
        _connection.Close();
    }

    public void Delete(int id)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "DELETE FROM answers WHERE id = :id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":id", id);

        adapter.SelectCommand?.ExecuteNonQuery();
        _connection.Close();
    }
}