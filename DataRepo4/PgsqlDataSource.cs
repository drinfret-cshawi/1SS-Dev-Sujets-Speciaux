using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataRepo4;

public class PgsqlDataSource : SqlDataSource
{
    public PgsqlDataSource(string connectionString) : base(connectionString)
    {
    }

    public PgsqlDataSource(string server, string db, string user, string password, int port) : base(server, db, user, password, port)
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

    public override DbParameter GetDbParameter(string name, SqlDbType sqlDbType, string? value)
    {
        DbParameter param = new NpgsqlParameter(name, sqlDbType);
        param.Value = value ?? (object) DBNull.Value;
        return param;
    }
    
    public override DbParameter GetIntDbParameter(string name, int? value)
    {
        DbParameter param = new NpgsqlParameter(name, SqlDbType.Int);
        param.Value = value ?? (object) DBNull.Value;
        return param;
    }


}