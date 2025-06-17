using BookStore.Application.Dtos.Authors;

namespace BookStore.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken token);
    Task<AuthorDto?> GetByIdAsync(Guid id, CancellationToken token);
    Task<AuthorDto> CreateAsync(AuthorCreateDto createDto, CancellationToken token);
    Task<bool> UpdateAsync(Guid id, AuthorUpdateDto updateDto, CancellationToken token);
    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}