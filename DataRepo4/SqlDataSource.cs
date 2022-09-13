using System.Data;
using System.Data.Common;

namespace DataRepo4;

public abstract class SqlDataSource : IDataSource
{
    protected string ConnectionString { get; set; }

    protected DbConnection? Connection { get; set; }

    protected SqlDataSource(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected SqlDataSource(string server, string db, string user, string password, int port)
        : this($"server={server};port={port};uid={user};pwd={password};database={db}")
    {
    }

    public abstract bool Connect();

    public abstract void Close();


    public abstract DbCommand GetCommand(string? sql);
    public abstract DbParameter GetDbParameter(string name, SqlDbType sqlDbType, string? value);
    public abstract DbParameter GetIntDbParameter(string name, int? value);
}