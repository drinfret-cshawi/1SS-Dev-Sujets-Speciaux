using System.Data;
using System.Data.Common;

namespace DataRepo2;

public abstract class PasswordRepo
{
    public static string? GetStringOrNull(DbDataReader reader, int col)
    {
        return Convert.IsDBNull(reader[col]) ? null : reader.GetString(col);
    }

    protected string ConnectionString { get; set; }

    protected DbConnection? Connection { get; set; }

    protected PasswordRepo(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public abstract bool Connect();

    protected abstract DbCommand GetCommand(string? sql);
    protected abstract DbParameter GetDbParameter(string name, SqlDbType sqlDbType, int value);

    public List<User> GetUsers()
    {
        string sql = "select * from users";
        var results = new List<User>();

        using var cmd = GetCommand(sql);
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

        return results;
    }

    public List<Password> GetPasswords()
    {
        string sql = "select * from passwords";
        var results = new List<Password>();

        using var cmd = GetCommand(sql);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new Password(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                GetStringOrNull(reader, 3),
                reader.GetString(4)));
        }

        reader.Close();

        return results;
    }

    public List<Password> GetPasswords(int userId)
    {
        string sql = "select * from passwords where user_id = @userId";
        var results = new List<Password>();

        using var cmd = GetCommand(null);
        cmd.CommandText = sql;
        cmd.Parameters.Add(GetDbParameter("@userId", SqlDbType.Int, userId));
        cmd.Prepare();

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new Password(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                GetStringOrNull(reader, 3),
                reader.GetString(4)));
        }

        reader.Close();

        return results;
    }
}