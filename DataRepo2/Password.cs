namespace DataRepo2;

public class Password
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Site { get; set; }
    public string? Login { get; set; }
    public string Pswd { get; set; }

    public Password(int id, int userId, string site, string? login, string pswd)
    {
        Id = id;
        UserId = userId;
        Site = site;
        Login = login;
        Pswd = pswd;
    }

    public override string ToString()
    {
        return $"{Id}, {UserId}, {Site}, {Login}, {Pswd}";
    }
}