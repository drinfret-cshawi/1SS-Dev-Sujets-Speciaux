namespace DataRepo3;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? FullName { get; set; }
    public string Pswd { get; set; }
    public string? Email { get; set; }

    public User(int id, string userName, string? fullName, string pswd, string? email)
    {
        Id = id;
        UserName = userName;
        FullName = fullName;
        Pswd = pswd;
        Email = email;
    }
    
    public override string ToString()
    {
        string fullName = FullName ?? "<null>";
        string email = Email ?? "<null>";
        return $"{Id}, {UserName}, {fullName}, {Pswd}, {email}";
    }
}