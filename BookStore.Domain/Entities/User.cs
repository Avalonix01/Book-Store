namespace BookStore.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; }

    public User(string userName, string passwordHash)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        PasswordHash = passwordHash;
    }
}
