namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Author Author { get; private set; }
    public Guid AuthorId { get; init; }

    private Book() {} // Just for EF to work properly

    public Book(string title, string description, Guid authorId, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
        AuthorId = authorId;
    }

    public void Update(string title, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty");
        }

        Title = title;
        Description = description;
        Price = price;
    }
}