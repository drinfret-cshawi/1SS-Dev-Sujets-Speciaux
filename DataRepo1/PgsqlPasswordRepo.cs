using Npgsql;

namespace DataRepo1;

public class PgsqlPasswordRepo : PasswordRepo
{
    public PgsqlPasswordRepo(string connectionString) : base(connectionString)
    {
    }

    public override bool Connect()
    {
        try
        {
            Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public override List<User> GetUsers()
    {
        string sql = "select * from users";
        var results = new List<User>();

        using (var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)Connection!))
        {
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new User(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    GetStringOrNull(reader, 2),
                    reader.GetString(3),
                    GetStringOrNull(reader, 4)));
            }

            reader.Close();
        }

        return results;
    }

    public override List<Password> GetPasswords()
    {
        string sql = "select * from passwords";
        var results = new List<Password>();

        using (var cmd = new NpgsqlCommand(sql, (NpgsqlConnection)Connection!))
        {
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new Password(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)));
            }

            reader.Close();
        }

        return results;
    }

    public override List<Password> GetPasswords(int userId)
    {
        string sql = "select * from passwords where user_id = @userId";
        var results = new List<Password>();

        using (var cmd = new NpgsqlCommand(null, (NpgsqlConnection)Connection!))
        {
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new Password(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)));
            }

            reader.Close();
        }

        return results;
    }
}