namespace TestCryptoLib;

using CryptoLib;

public class UnitTestPasswordGenerator
{
    [Fact]
    public void TestGeneratePasswordLength5()
    {
        int expected = 5;
        string password = PasswordGenerator.GeneratePassword(expected);

        Assert.Equal(expected, password.Length);
    }

    [Fact]
    public void TestGeneratePasswordLength()
    {
        int[] given = { -1, 0, 1, 5, 10 };
        int[] expected = { 0, 0, 1, 5, 10 };
        for (int i = 0; i < given.Length; i++)
        {
            string password = PasswordGenerator.GeneratePassword(given[i]);
            Assert.Equal(expected[i], password.Length);
        }
    }

    [Fact]
    public void TestGeneratePasswordAllFalseParams()
    {
        Assert.Throws<ArgumentException>(
            () => PasswordGenerator.GeneratePassword(5, false, false, false, false)
        );
    }

    [Fact]
    public void TestGeneratePasswordOneTrueParam()
    {
        int length = 50;
        string password = PasswordGenerator.GeneratePassword(length, true, false, false, false);
        Assert.True(password.All(c => PasswordGenerator.Symbols.Contains(c)));
        // Assert.DoesNotContain(password, c => PasswordGenerator.Digits.Contains(c));
        // Assert.DoesNotContain(password, c => PasswordGenerator.UppercaseLetters.Contains(c));
        // Assert.DoesNotContain(password, c => PasswordGenerator.LowercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, false, true, false, false);
        Assert.True(password.All(c => PasswordGenerator.Digits.Contains(c)));

        password = PasswordGenerator.GeneratePassword(length, false, false, true, false);
        Assert.True(password.All(c => PasswordGenerator.UppercaseLetters.Contains(c)));

        password = PasswordGenerator.GeneratePassword(length, false, false, false, true);
        Assert.True(password.All(c => PasswordGenerator.LowercaseLetters.Contains(c)));
    }

    [Fact]
    public void TestGeneratePasswordTwoTrueParams()
    {
        int length = 50;
        string password = PasswordGenerator.GeneratePassword(length, true, true, false, false);
        Assert.DoesNotContain(password, c => PasswordGenerator.UppercaseLetters.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.LowercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, true, false, true, false);
        Assert.DoesNotContain(password, c => PasswordGenerator.Digits.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.LowercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, true, false, false, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.Digits.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.UppercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, false, true, true, false);
        Assert.DoesNotContain(password, c => PasswordGenerator.Symbols.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.LowercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, false, true, false, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.Symbols.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.UppercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, false, false, true, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.Symbols.Contains(c));
        Assert.DoesNotContain(password, c => PasswordGenerator.Digits.Contains(c));
    }

    [Fact]
    public void TestGeneratePasswordThreeTrueParams()
    {
        int length = 50;
        string password = PasswordGenerator.GeneratePassword(length, true, true, true, false);
        Assert.DoesNotContain(password, c => PasswordGenerator.LowercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, true, true, false, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.UppercaseLetters.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, true, false, true, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.Digits.Contains(c));

        password = PasswordGenerator.GeneratePassword(length, false, true, true, true);
        Assert.DoesNotContain(password, c => PasswordGenerator.Symbols.Contains(c));
    }
}