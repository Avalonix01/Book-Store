using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(CancellationToken token);
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token);
    Task AddBookAsync(Book book, CancellationToken token);
    Task UpdateBookAsync(Book book, CancellationToken token);
    Task DeleteBookAsync(Guid id, CancellationToken token);
}