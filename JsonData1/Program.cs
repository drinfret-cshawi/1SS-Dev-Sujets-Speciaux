using System.Text.Json;

namespace JsonData1;

public class TestAesCrypter
{
    public static void Main(string[] args)
    {
        List<Password> passwords = new List<Password>()
        {
            new("denis", "12345678"),
            new("alice", "password"),
            new("bob", "bob123bob")
        };
        
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(passwords, options);
        Console.WriteLine(json);

        List<Password> passwords2 = JsonSerializer.Deserialize<List<Password>>(json) ?? new List<Password>();
        Console.WriteLine(string.Join("\n", passwords2));
        
    }
}