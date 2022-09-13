using System.Data;
using System.Data.Common;

namespace DataRepo4;

public class SqlUserRepo : SqlDataRepo<User, int>
{
    public static User CreateUser(DbDataReader reader)
    {
        return new User(
            reader.GetInt32(0),
            reader.GetString(1),
            GetStringOrNull(reader, 2),
            reader.GetString(3),
            GetStringOrNull(reader, 4));
    }

    public SqlUserRepo(SqlDataSource sqlDataSource) : base(sqlDataSource)
    {
    }

    public override List<User> Select()
    {
        string sql = "select * from users";
        var results = new List<User>();

        using var cmd = SqlDataSource.GetCommand(sql);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(CreateUser(reader));
        }

        reader.Close();

        return results;
    }

    public override User? Select(int id)
    {
        User? user = null;
        string sql = "select * from users where id = @id";

        using var cmd = SqlDataSource.GetCommand(null);
        cmd.CommandText = sql;
        cmd.Parameters.Add(SqlDataSource.GetIntDbParameter("@id", id));
        cmd.Prepare();

        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            user = CreateUser(reader);
        }

        reader.Close();

        return user;
    }
    

    public override int Insert(User data)
    {
        string sql =
            "insert into users(username, fullname, password, email) values (@userName, @fullName, @password, @email) returning id";
    
        using var cmd = SqlDataSource.GetCommand(null);
        cmd.CommandText = sql;
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@userName", SqlDbType.Text, data.UserName));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@fullName", SqlDbType.Text, data.FullName));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@password", SqlDbType.Text, data.Pswd));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@email", SqlDbType.Text, data.Email));
        cmd.Prepare();
        try
        {
            int id = Convert.ToInt32(cmd.ExecuteScalar()?.ToString());
            return id;
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public override bool Update(User data)
    {
        string sql =
            "update users set username = @userName, fullname = @fullName, password = @password, email = @email where id = @id";

        using var cmd = SqlDataSource.GetCommand(null);
        cmd.CommandText = sql;
        cmd.Parameters.Add(SqlDataSource.GetIntDbParameter("@id", data.Id));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@userName", SqlDbType.Text, data.UserName));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@fullName", SqlDbType.Text, data.FullName));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@password", SqlDbType.Text, data.Pswd));
        cmd.Parameters.Add(SqlDataSource.GetDbParameter("@email", SqlDbType.Text, data.Email));
        cmd.Prepare();
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public override bool Delete(User data)
    {
        return Delete(data.Id);
    }

    public override bool Delete(int key)
    {
        string sql = "delete from users where id = @id";

        using var cmd = SqlDataSource.GetCommand(null);
        cmd.CommandText = sql;
        cmd.Parameters.Add(SqlDataSource.GetIntDbParameter("@id", key));
        cmd.Prepare();
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}