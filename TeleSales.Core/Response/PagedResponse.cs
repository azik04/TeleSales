namespace TeleSales.Core.Response;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNextPage => (CurrentPage * PageSize) < TotalCount;
    public bool HasPreviousPage => CurrentPage > 1;
}
