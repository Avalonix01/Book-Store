using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken token);
    Task RegisterUserAsync(User user, CancellationToken token);
}
