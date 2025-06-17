using BookStore.Application.Dtos.Auth;

namespace BookStore.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDto loginDto, CancellationToken token);
    Task<string?> RegisterAsync(RegisterDto registerDto, CancellationToken token);
}
