namespace ShopHub.Infrastructure.Shared.Pagination;

public class PagedRequest
{
    private int _pageSize = 20;
    private int _page = 1;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 100 ? 100 : value < 1 ? 1 : value;
    }

    public int Skip => (Page - 1) * PageSize;
}