using System.Security.Claims;
using BookStore.Application.Dtos.Auth;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Serilog;

namespace BookStore.Application.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public AuthService(IJwtService jwtService, IUserRepository userRepository, ILogger logger)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<string?> LoginAsync(LoginDto loginDto, CancellationToken token)
    {
        var user = await _userRepository.GetByUserNameAsync(loginDto.UserName, token)
                   ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            _logger.Warning("Login attempt failed for user {UserName}", loginDto.UserName);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        _logger.Information("User {UserName} logged in successfully", user.UserName);
        return _jwtService.GenerateToken(claims);
    }

    public async Task<string?> RegisterAsync(RegisterDto registerDto, CancellationToken token)
    {
        var user = await _userRepository.GetByUserNameAsync(registerDto.UserName, token);

        if (user != null)
        {
            _logger.Warning("Registration attempt failed: User with username {UserName} already exists",
                registerDto.UserName);

            throw new InvalidOperationException("User with that username already exists.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var newUser = new User(registerDto.UserName, hashedPassword);

        _logger.Information("Registering new user with UserName: {UserName}", registerDto.UserName);

        await _userRepository.RegisterUserAsync(newUser, token);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, newUser.UserName)
        };

        _logger.Information("User {UserName} registered successfully", newUser.UserName);
        return _jwtService.GenerateToken(claims);
    }
}
