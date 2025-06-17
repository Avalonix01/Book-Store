using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class BookRepository(StoreDbContext context) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetBooksAsync(CancellationToken token)
    {
        var books = await context.Books
            .AsNoTracking()
            .ToListAsync(token);

        return books;
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token)
    {
        var book = await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(book => book.Id == id, token);

        return book;
    }

    public async Task AddBookAsync(Book book, CancellationToken token)
    {
        await context.Books.AddAsync(book, token);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateBookAsync(Book book, CancellationToken token)
    {
        var existingBook = await context.Books
            .FirstOrDefaultAsync(a => a.Id == book.Id, token);

        if (existingBook != null)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync(token);
        }
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken token)
    {
        var book = await context.Books
            .FirstOrDefaultAsync(a => a.Id == id, token);

        if (book != null)
        {
            await context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync(token);
        }
    }
}