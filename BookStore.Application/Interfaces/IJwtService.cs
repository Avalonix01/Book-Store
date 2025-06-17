using System.Security.Claims;

namespace BookStore.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(IEnumerable<Claim> claims);
}
