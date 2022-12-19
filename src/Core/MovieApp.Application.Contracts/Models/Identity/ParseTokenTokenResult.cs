namespace MovieApp.Application.Contracts.Models.Identity;

public class ParseTokenResult
{
    public bool IsValid { get; set; }
    public int UserId { get; set; }
    public DateTime ExpDate { get; set; }
}