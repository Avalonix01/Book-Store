using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<StoreDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
