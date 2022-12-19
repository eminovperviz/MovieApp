namespace MovieApp.Application.Contracts.DTOs;

public sealed class DropDownDto : BaseDTO
{
    public int Value { get; set; }
    public string DisplayText { get; set; }
    public bool Select { get; set; }
}
