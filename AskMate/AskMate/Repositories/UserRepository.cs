using System.Data;
using AskMate.Model;
using Npgsql;

namespace AskMate.Repositories;

public class UserRepository
{
    private readonly NpgsqlConnection _connection;

    public UserRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }   
    public int Create(User user)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter(
            "INSERT INTO users (username, email, password, registration_time) " +
            "VALUES (:username, :email, :password, :registration_time) RETURNING id",
            _connection
        );
        adapter.SelectCommand?.Parameters.AddWithValue(":username", user.Username);
        adapter.SelectCommand?.Parameters.AddWithValue(":email", user.Email);
        adapter.SelectCommand?.Parameters.AddWithValue(":password", user.Password);
        adapter.SelectCommand?.Parameters.AddWithValue(":registration_time", user.Registration_time);

        var lastInsertId = (int)adapter.SelectCommand?.ExecuteScalar();
        _connection.Close();

        return lastInsertId;
    }
    public User AuthenticateUser(string username, string password)
    {
        _connection.Open();
        var adapter = new NpgsqlDataAdapter("SELECT * FROM users WHERE username = :username AND password = :password", _connection);
        adapter.SelectCommand?.Parameters.AddWithValue(":username", username);
        adapter.SelectCommand?.Parameters.AddWithValue(":password", password);
        var dataSet = new DataSet();
        adapter.Fill(dataSet);
        var table = dataSet.Tables[0];
        
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            return new User()
            {
                Id = (int)row["id"],
                Username = (string)row["username"],
                Email = (string)row["email"],
                Password = (string)row["password"],
                Registration_time = (DateTime)row["registration_time"]
            };
            
        }
        
        
        _connection.Close();

        return null;
        // return true;
    }
}