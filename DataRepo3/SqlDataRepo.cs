using System.Data;
using System.Data.Common;

namespace DataRepo3;

public abstract class SqlDataRepo<TData, TKey> : IDataRepo<TData, TKey>
{
    public static string? GetStringOrNull(DbDataReader reader, int col)
    {
        return Convert.IsDBNull(reader[col]) ? null : reader.GetString(col);
    }

    protected string ConnectionString { get; set; }

    protected DbConnection? Connection { get; set; }

    public SqlDataRepo(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public SqlDataRepo(string server, string db, string user, string password, int port)
        : this($"server={server};port={port};uid={user};pwd={password};database={db}")
    {
    }

    public abstract bool Connect();

    public abstract void Close();
    
    public abstract DbCommand GetCommand(string? sql);
    
    public abstract DbParameter GetDbParameter(string name, SqlDbType sqlDbType, int value);

    public abstract List<TData> Select();

    public abstract TData? Select(TKey id);

    public abstract bool Insert(TData data);

    public abstract bool Update(TData data);

    public abstract bool Delete(TData data);

    public abstract bool Delete(TKey key);
}