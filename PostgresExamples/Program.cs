// Source: https://zetcode.com/csharp/postgresql/

using Npgsql;

namespace PostgresExamples;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Host=localhost;Username=passman;Password=passman;Database=passman";

        using var con = new NpgsqlConnection(connectionString);
        con.Open();

        var sql = "SELECT version()";

        using (var cmd = new NpgsqlCommand(sql, con))
        {
            var version = cmd.ExecuteScalar()?.ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
        }

        sql = "select * from passwords";
        
        using (var cmd = new NpgsqlCommand(sql, con))
        {
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", 
                    reader.GetInt32(0), 
                    reader.GetInt32(1), 
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4));
            }
            reader.Close();
        }
        
        sql = "select * from users";
        
        using (var cmd = new NpgsqlCommand(sql, con))
        {
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", 
                    reader.GetInt32(0), 
                    reader.GetString(1), 
                    reader.GetString(2),
                    reader.GetString(3),
                    Convert.IsDBNull(reader[4]) ? "<null>" :reader.GetString(4));
            }
            reader.Close();
            
        }
    }
}