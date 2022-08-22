using System.Security.Cryptography;

namespace Crypto2;

public class TestCrypto
{
    public static void Main(string[] args)
    {
        try
        {
            using (FileStream fileStream = new("TestData.txt", FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                    };
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new(
                               fileStream,
                               aes.CreateEncryptor(),
                               CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new(cryptoStream))
                        {
                            encryptWriter.WriteLine("Hello World!");
                        }
                    }
                }
            }

            Console.WriteLine("The file was encrypted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The encryption failed. {ex}");
        }


        try
        {
            using (FileStream fileStream = new("TestData.txt", FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.IV.Length];
                    int numBytesToRead = aes.IV.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                        if (n == 0) break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }

                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                    };

                    using (CryptoStream cryptoStream = new(
                               fileStream,
                               aes.CreateDecryptor(key, iv),
                               CryptoStreamMode.Read))
                    {
                        using (StreamReader decryptReader = new(cryptoStream))
                        {
                            string decryptedMessage = decryptReader.ReadToEnd();
                            Console.WriteLine($"The decrypted original message: {decryptedMessage}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The decryption failed. {ex}");
        }

        Aes aes2 = Aes.Create();
        Console.WriteLine(string.Join(" ", aes2.IV.Select(b => $"{b:X}")));
        Console.WriteLine(string.Join(" ", aes2.Key.Select(b => $"{b:X}")));
        aes2.GenerateIV();
        Console.WriteLine();
        Console.WriteLine(string.Join(" ", aes2.IV.Select(b => $"{b:X}")));
        Console.WriteLine(string.Join(" ", aes2.Key.Select(b => $"{b:X}")));
        aes2.GenerateKey();
        Console.WriteLine();
        Console.WriteLine(string.Join(" ", aes2.IV.Select(b => $"{b:X}")));
        Console.WriteLine(string.Join(" ", aes2.Key.Select(b => $"{b:X}")));
        
    }
}