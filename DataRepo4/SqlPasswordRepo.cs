using System.Data.Common;

namespace DataRepo4;

public class SqlPasswordRepo : SqlDataRepo<Password, int>
{
    public static Password CreatePassword(DbDataReader reader)
    {
        return new Password(
            reader.GetInt32(0),
            reader.GetInt32(1),
            reader.GetString(2),
            GetStringOrNull(reader, 3),
            reader.GetString(4));
    }

    public SqlPasswordRepo(SqlDataSource sqlDataSource) : base(sqlDataSource)
    {
    }

    public override List<Password> Select()
    {
        string sql = "select * from passwords";
        var results = new List<Password>();

        using var cmd = SqlDataSource.GetCommand(sql);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(CreatePassword(reader));
        }

        reader.Close();

        return results;
    }

    public override Password? Select(int id)
    {
        throw new NotImplementedException();
    }

    public override int Insert(Password data)
    {
        throw new NotImplementedException();
    }

    public override bool Update(Password data)
    {
        throw new NotImplementedException();
    }

    public override bool Delete(Password data)
    {
        throw new NotImplementedException();
    }

    public override bool Delete(int key)
    {
        throw new NotImplementedException();
    }
}