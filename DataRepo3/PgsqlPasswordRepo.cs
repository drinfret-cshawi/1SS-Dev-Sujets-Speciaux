namespace DataRepo3;

public class PgsqlPasswordRepo : PgsqlRepo<Password, int>
{
    public PgsqlPasswordRepo(string connectionString) : base(connectionString)
    {
    }

    public PgsqlPasswordRepo(string server, string db, string user, string password, int port = 5432) : base(server, db, user, password, port)
    {
    }

    public override List<Password> Select()
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

    public override Password? Select(int id)
    {
        throw new NotImplementedException();
    }

    public override bool Insert(Password data)
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