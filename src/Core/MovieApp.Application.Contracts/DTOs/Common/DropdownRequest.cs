namespace MovieApp.Application.Contracts.DTOs;

public sealed class DropdownRequest : PagingRequest
{
    public DropdownRequest()
    {

    }

    public int? DependentColumnId { get; set; }

    public string SearchKey { get; set; }

    public DropdownRequest(string searchKey)
    {
        SearchKey = searchKey;
    }

}
