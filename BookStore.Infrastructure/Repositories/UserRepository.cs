using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class UserRepository(StoreDbContext context) : IUserRepository
{
    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken token)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.UserName == userName, token);

        return user;
    }

    public async Task RegisterUserAsync(User user, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        await context.Users.AddAsync(user, token);
        await context.SaveChangesAsync(token);
    }
}
