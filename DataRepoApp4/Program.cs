using DataRepo4;

namespace DataRepoApp4;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Host=localhost;Username=denis;Password=denis;Database=denis";
        PgsqlDataSource dataSource = new PgsqlDataSource(connectionString);
        if (!dataSource.Connect())
        {
            Console.WriteLine("Cannot connect to DB. Exiting.");
            return;
        }
        
        SqlUserRepo userRepo = new SqlUserRepo(dataSource);
        SqlPasswordRepo passwordRepo = new SqlPasswordRepo(dataSource);

        Console.WriteLine("All users");
        var users = userRepo.Select();
        Console.WriteLine(string.Join("\n", users));

        Console.WriteLine("User 1");
        var user = userRepo.Select(1);
        Console.WriteLine(user);
        
        Console.WriteLine("User 2");
        user = userRepo.Select(2);
        Console.WriteLine(user);
        
        Console.WriteLine("User 3");
        user = userRepo.Select(3);
        Console.WriteLine(user);

        user = new User(0, "carl", null, "aaaabbbb", null);
        Console.WriteLine($"Inserting User : {user}");
        int id = userRepo.Insert(user);
        Console.WriteLine($"id = {id}");
        
        Console.WriteLine("All users");
        users = userRepo.Select();
        Console.WriteLine(string.Join("\n", users));

        user.Id = id;
        user.FullName = "Carl Carl";
        Console.WriteLine($"Updating User : {user}");
        Console.WriteLine(userRepo.Update(user));
        
        Console.WriteLine("All users");
        users = userRepo.Select();
        Console.WriteLine(string.Join("\n", users));
        
        Console.WriteLine($"Deleting User : {user}");
        Console.WriteLine(userRepo.Delete(user));
        
        Console.WriteLine("All users");
        users = userRepo.Select();
        Console.WriteLine(string.Join("\n", users));
        
    }
}