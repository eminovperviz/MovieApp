using MovieApp.Application.Contracts.Models.Identity;
using System.Security.Claims;

namespace MovieApp.Application.Contracts.Interfaces;

public interface IJwtService
{
    JwtTokenResult GenerateToken(List<Claim> claims);
    ParseTokenResult ValidateToken(string token);
    bool CanReadToken(string token);
}
