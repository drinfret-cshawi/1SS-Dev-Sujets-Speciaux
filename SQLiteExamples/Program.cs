// Sources: https://zetcode.com/csharp/postgresql/
// https://zetcode.com/csharp/sqlite/

using System.Data.SQLite;

namespace SQLiteExamples;

public class Program
{
    public static void Main(string[] args)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.sqlite");
        var connectionString = "URI=file:" + path;

        using var con = new SQLiteConnection(connectionString);
        con.Open();

        var sql = "SELECT sqlite_version()";

        using (var cmd = new SQLiteCommand(sql, con))
        {
            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"SQLite version: {version}");
        }

        sql = "select * from passwords";
        
        using (var cmd = new SQLiteCommand(sql, con))
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
        
        using (var cmd = new SQLiteCommand(sql, con))
        {
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", 
                    reader.GetInt32(0), 
                    reader.GetString(1), 
                    reader.GetString(2),
                    reader.GetString(3),
                    Convert.IsDBNull(reader[4]) ? "<null>" : reader.GetString(4));
            }
            reader.Close();
            
        }
    }
}