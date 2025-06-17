namespace BookStore.Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public List<Book> Books { get; init; } = [];
    
    private Author() {} // Just for EF to work properly

    public Author(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
    
    public void Update(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Surname cannot be empty");
        }

        Name = name;
        Surname = surname;
    }
}