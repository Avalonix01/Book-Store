using BookStore.Application.Dtos.Books;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Mapster;
using Serilog;

namespace BookStore.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger _logger;

    public BookService(IBookRepository bookRepository, ILogger logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken token)
    {
        var bookEntities = await _bookRepository.GetBooksAsync(token);
        var books = bookEntities.ToList();

        if (!books.Any())
        {
            _logger.Warning("No books found in the repository");
            return [];
        }

        _logger.Information("Retrieved {Count} books from the repository", books.Count());
        return bookEntities.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<BookDto?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var bookEntity = await _bookRepository.GetBookByIdAsync(id, token);

        if (bookEntity == null)
        {
            _logger.Warning("Book with ID {Id} not found", id);
            return null;
        }

        _logger.Information("Retrieved book with ID {Id} from the repository", id);
        return bookEntity.Adapt<BookDto?>();
    }

    public async Task<BookDto> CreateAsync(BookCreateDto createDto, CancellationToken token)
    {
        var bookEntity = new Book(createDto.Title, createDto.Description, createDto.AuthorId, createDto.Price);

        _logger.Information("Creating new book with Title: {Title} ", createDto.Title);

        await _bookRepository.AddBookAsync(bookEntity, token);
        _logger.Information("Created new book with ID {Id}", bookEntity.Id);

        return bookEntity.Adapt<BookDto>();
    }

    public async Task<bool> UpdateAsync(Guid id, BookUpdateDto updateDto, CancellationToken token)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(id, token);

        if (existingBook == null)
        {
            _logger.Warning("Book with ID {Id} not found for update", id);
            return false;
        }

        existingBook.Update(updateDto.Title, updateDto.Description, updateDto.Price);
        await _bookRepository.UpdateBookAsync(existingBook, token);

        _logger.Information("Updated book with ID {Id}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(id, token);

        if (existingBook == null)
        {
            _logger.Warning("Book with ID {Id} not found for deletion", id);
            return false;
        }

        await _bookRepository.DeleteBookAsync(id, token);
        _logger.Information("Deleted book with ID {Id}", id);

        return true;
    }
}