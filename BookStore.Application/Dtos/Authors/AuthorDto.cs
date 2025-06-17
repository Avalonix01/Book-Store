using BookStore.Application.Dtos.Books;

namespace BookStore.Application.Dtos.Authors;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public List<BookDto> Books { get; set; } = new();
}