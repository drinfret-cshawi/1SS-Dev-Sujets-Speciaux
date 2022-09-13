using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataRepo3;

public abstract class PgsqlRepo<TData, TKey> : SqlDataRepo<TData, TKey>
{
    public PgsqlRepo(string connectionString) : base(connectionString)
    {
    }

    public PgsqlRepo(string server, string db, string user, string password, int port = 5432) 
        : base(server, db, user, password, port)
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

    public override void Close()
    {
        Connection!.Close();
    }

    public override DbCommand GetCommand(string? sql)
    {
        return new NpgsqlCommand(sql, (NpgsqlConnection)Connection!);
    }

    public override DbParameter GetDbParameter(string name, SqlDbType sqlDbType, int value)
    {
        DbParameter param = new NpgsqlParameter(name, sqlDbType);
        param.Value = value;
        return param;
    }
    
}