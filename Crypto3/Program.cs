
using CryptoLib;

namespace Crypto3;

public class TestAesCrypter
{
    public static void Main(string[] args)
    {
        AesCrypter aesCrypter = new AesCrypter("c'est ma clé");
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.dat");
        string data = "Allo";
        aesCrypter.EncryptToFile(data, path);
        string decrypted = aesCrypter.DecryptFromFile(path);
        Console.WriteLine($"{data} {decrypted}");
        Console.WriteLine();
        
        string encrypted = aesCrypter.Encrypt(data);
        decrypted = aesCrypter.Decrypt(encrypted);
        Console.WriteLine($"{data} {encrypted} {decrypted}");
    }
}