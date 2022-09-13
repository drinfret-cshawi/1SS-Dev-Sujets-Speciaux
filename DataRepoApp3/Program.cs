using DataRepo3;

namespace DataRepoApp3;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Host=localhost;Username=denis;Password=denis;Database=denis";

        PgsqlUserRepo userRepo = new PgsqlUserRepo(connectionString);
        PgsqlPasswordRepo passwordRepo = new PgsqlPasswordRepo(connectionString);

        if (!userRepo.Connect())
        {
            Console.WriteLine("Cannot connect to DB. Exiting.");
            return;
        }
        
        if (!passwordRepo.Connect())
        {
            Console.WriteLine("Cannot connect to DB. Exiting.");
            return;
        }

        Console.WriteLine("All users");
        var users = userRepo.Select();
        Console.WriteLine(string.Join("\n", users));

        Console.WriteLine("All passwords");
        var passwords = passwordRepo.Select();
        Console.WriteLine(string.Join("\n", passwords));

        Console.WriteLine("Passwords of user 1");
        var password = passwordRepo.Select(1);
        Console.WriteLine(password);
        
        Console.WriteLine("Passwords of user 2");
        password = passwordRepo.Select(2);
        Console.WriteLine(passwords);
        
        Console.WriteLine("Passwords of user 3");
        password = passwordRepo.Select(3);
        Console.WriteLine(password);
    }
}