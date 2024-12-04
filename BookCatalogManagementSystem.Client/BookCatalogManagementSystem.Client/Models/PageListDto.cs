namespace BookCatalogManagementSystem.Client.Models;

public class PageListDto<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasPropertyInNextPage { get; set; }
    public bool HasNext => CurrentPage < TotalPages;
    public List<T> Items { get; set; }
}