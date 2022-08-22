// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using CryptoLib;

namespace Crypto1
{
    internal class Program
    {
        static string letters = "abcdef";

        static string GeneratePassword(int length)
        {
            int i = RandomNumberGenerator.GetInt32(0, letters.Length);
            return letters[i].ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GeneratePassword(8));
            // Console.WriteLine(RandomNumberGenerator.GetInt32(0, 100));
            // Console.WriteLine(RandomNumberGenerator.GetInt32(0, 100));
            // Console.WriteLine(RandomNumberGenerator.GetInt32(0, 100));

            for (int i = 1; i <= 32; i++)
            {
                string password = PasswordGenerator.GeneratePassword(i);
                bool isValid = PasswordGenerator.ValidatePassword(password);
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);
                bool correct = BCrypt.Net.BCrypt.Verify(password,hashed);
                Console.WriteLine($"Valid? {isValid}  {password} {hashed} {correct}");
            }
            
        }
    }
}