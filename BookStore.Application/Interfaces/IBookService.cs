using BookStore.Application.Dtos.Books;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken token);
    
    Task<BookDto?> GetByIdAsync(Guid id, CancellationToken token);
    
    Task<BookDto> CreateAsync(BookCreateDto createDto, CancellationToken token);
    
    Task<bool> UpdateAsync(Guid id, BookUpdateDto updateDto, CancellationToken token);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken token);
}