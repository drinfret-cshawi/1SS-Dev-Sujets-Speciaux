namespace DataRepo3;

public class PgsqlUserRepo : PgsqlRepo<User, int>
{
    public PgsqlUserRepo(string connectionString) : base(connectionString)
    {
    }

    public PgsqlUserRepo(string server, string db, string user, string password, int port = 5432) : base(server, db, user, password, port)
    {
    }

    public override List<User> Select()
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

    public override User? Select(int id)
    {
        throw new NotImplementedException();
    }

    public override bool Insert(User data)
    {
        throw new NotImplementedException();
    }

    public override bool Update(User data)
    {
        throw new NotImplementedException();
    }

    public override bool Delete(User data)
    {
        throw new NotImplementedException();
    }

    public override bool Delete(int key)
    {
        throw new NotImplementedException();
    }
}