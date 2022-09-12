using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataRepo2;

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

    protected override DbCommand GetCommand(string? sql)
    {
        return new NpgsqlCommand(sql, (NpgsqlConnection)Connection!);
    }

    protected override DbParameter GetDbParameter(string name, SqlDbType sqlDbType, int value)
    {
        DbParameter param = new NpgsqlParameter(name, sqlDbType);
        param.Value = value;
        return param;
    }
}