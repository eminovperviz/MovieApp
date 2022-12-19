namespace MovieApp.Application.Contracts.Models.Identity;

public class JwtTokenResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiryDate { get; set; }
    public DateTime RefreshTokenExpiryDate { get; set; }
}
