using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class AuthorRepository(StoreDbContext context) : IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken token)
    {
        var authors = await context.Authors
            .AsNoTracking()
            .Include(a => a.Books)
            .ToListAsync(token);

        return authors;
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken token)
    {
        var author = await context.Authors
            .AsNoTracking()
            .Include(a => a.Books)
            .FirstOrDefaultAsync(author => author.Id == id, token);

        return author;
    }

    public async Task AddAuthorAsync(Author author, CancellationToken token)
    {
        await context.Authors.AddAsync(author, token);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAuthorAsync(Author author, CancellationToken token)
    {
        var existingAuthor = await context.Authors
            .FirstOrDefaultAsync(a => a.Id == author.Id, token);

        if (existingAuthor != null)
        {
            existingAuthor.Update(author.Name, author.Surname);
            await context.SaveChangesAsync(token);
        }
    }

    public async Task DeleteAuthorAsync(Guid id, CancellationToken token)
    {
        var author = await context.Authors
            .FirstOrDefaultAsync(a => a.Id == id, token);

        if (author != null)
        {
            await context.Authors
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);              
        }
    }
}