using Asp.Versioning;
using BookStore.API.Endpoints;
using BookStore.Application.Dtos.Auth;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Authenticating user and returning JWT-token.
    /// </summary>
    /// <returns>JWT-token for authorization.</returns>
    [HttpPost(ApiEndpoints.V1.Auth.Login)]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto, CancellationToken token)
    {
        var jwtToken = await authService.LoginAsync(loginDto, token);
        return Ok(jwtToken);
    }

    /// <summary>
    /// Register new user and returns new JWT-token.
    /// </summary>
    /// <returns>JWT-token for authorization.</returns>
    [HttpPost(ApiEndpoints.V1.Auth.Register)]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto, CancellationToken token)
    {
        var jwtToken = await authService.RegisterAsync(registerDto, token);
        return Ok(jwtToken);
    }
}