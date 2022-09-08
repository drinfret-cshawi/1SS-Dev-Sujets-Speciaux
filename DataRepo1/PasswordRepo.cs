using System.Data.Common;

namespace DataRepo1;

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

    public abstract List<User> GetUsers();
    
    public abstract List<Password> GetPasswords();
    
    public abstract List<Password> GetPasswords(int userId);
}
