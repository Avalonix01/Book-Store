namespace BookStore.Application.Dtos.Books;

public class BookUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.0m;
}