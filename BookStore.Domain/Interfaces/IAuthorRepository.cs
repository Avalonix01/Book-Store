using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken token = default);
    Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken token = default);
    Task AddAuthorAsync(Author author, CancellationToken token = default);
    Task UpdateAuthorAsync(Author author, CancellationToken token = default);
    Task DeleteAuthorAsync(Guid id, CancellationToken token = default);
}