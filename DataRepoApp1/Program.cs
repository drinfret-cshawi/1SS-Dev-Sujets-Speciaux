using DataRepo1;

namespace DataRepoApp1;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Host=localhost;Username=denis;Password=denis;Database=denis";

        PgsqlPasswordRepo repo = new PgsqlPasswordRepo(connectionString);

        if (!repo.Connect())
        {
            Console.WriteLine("Cannot connect to DB. Exiting.");
            return;
        }

        Console.WriteLine("All users");
        var users = repo.GetUsers();
        Console.WriteLine(string.Join("\n", users));

        Console.WriteLine("All passwords");
        var passwords = repo.GetPasswords();
        Console.WriteLine(string.Join("\n", passwords));

        Console.WriteLine("Passwords of user 1");
        passwords = repo.GetPasswords(1);
        Console.WriteLine(string.Join("\n", passwords));
        
        Console.WriteLine("Passwords of user 2");
        passwords = repo.GetPasswords(2);
        Console.WriteLine(string.Join("\n", passwords));
        
        Console.WriteLine("Passwords of user 3");
        passwords = repo.GetPasswords(3);
        Console.WriteLine(string.Join("\n", passwords));
    }
}