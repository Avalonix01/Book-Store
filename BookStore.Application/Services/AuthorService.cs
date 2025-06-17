using BookStore.Application.Dtos.Authors;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Mapster;
using Serilog;

namespace BookStore.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILogger _logger;

    public AuthorService(IAuthorRepository authorRepository, ILogger logger)
    {
        _authorRepository = authorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken token = default)
    {
        var authorEntities = await _authorRepository.GetAuthorsAsync(token);
        var authors = authorEntities.ToList();

        if (!authors.Any())
        {
            _logger.Warning("No authors found in the repository");
            return [];
        }

        _logger.Information("Retrieved {Count} authors from the repository", authors.Count());
        return authorEntities.Adapt<IEnumerable<AuthorDto>>();
    }

    public async Task<AuthorDto?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var authorEntity = await _authorRepository.GetAuthorByIdAsync(id, token);

        if (authorEntity == null)
        {
            _logger.Warning("Author with ID {Id} not found", id);
            return null;
        }

        _logger.Information("Retrieved author with ID {Id} from the repository", id);
        return authorEntity?.Adapt<AuthorDto>();
    }

    public async Task<AuthorDto> CreateAsync(AuthorCreateDto createDto, CancellationToken token)
    {
        var author = new Author(createDto.Name, createDto.Surname);

        _logger.Information("Creating new author with Name: {Name}, Surname: {Surname}",
            createDto.Name, createDto.Surname);

        await _authorRepository.AddAuthorAsync(author, token);
        _logger.Information("Created new author with ID {Id}", author.Id);

        return author.Adapt<AuthorDto>();
    }

    public async Task<bool> UpdateAsync(Guid id, AuthorUpdateDto updateDto, CancellationToken token)
    {
        var existingAuthor = await _authorRepository.GetAuthorByIdAsync(id, token);

        if (existingAuthor is null)
        {
            _logger.Warning("Author with ID {Id} not found for update", id);
            return false;
        }

        existingAuthor.Update(updateDto.Name, updateDto.Surname);
        await _authorRepository.UpdateAuthorAsync(existingAuthor, token);

        _logger.Information("Updated author with ID {Id}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(id, token);

        if (author is null)
        {
            _logger.Warning("Tried to delete non-existent author with ID {Id}", id);
            return false;
        }

        await _authorRepository.DeleteAuthorAsync(id, token);
        _logger.Information("Deleted author with ID {Id}", id);

        return true;
    }
}