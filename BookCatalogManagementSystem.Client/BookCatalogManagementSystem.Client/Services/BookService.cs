using System.Net.Http.Json;
using System.Text.Json;
using BookCatalogManagementSystem.Client.Models;
using BookCatalogManagementSystem.Client.Models.Constants;

namespace BookCatalogManagementSystem.Client.Services;

public class BookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PageListDto<BookDto>> FetchBooks(int currentPage, int pageSize, string sortColumn,
        bool isAscending, string searchQuery)
    {
        try
        {
            var sortDirection = isAscending ? "asc" : "desc";
            var apiUrl =
                $"{ApiUrlAddress.ApiBaseAddress}/?pageNumber={currentPage}&pageSize={pageSize}&sortField={sortColumn}&sortDirection={sortDirection}";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                apiUrl += $"&searchValue={searchQuery}";
            }
            
            var response = await _httpClient.GetFromJsonAsync<string>(apiUrl);

            var data = JsonSerializer.Deserialize<PageListDto<BookDto>>(response);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching books: {ex.Message}");
            return null;
        }
    }
    
    public async Task<bool> CreateBook(BookDto bookDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrlAddress.ApiBaseAddress}/", bookDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating book: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateBook(BookDto bookDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrlAddress.ApiBaseAddress}/", bookDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteBook(int bookId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrlAddress.ApiBaseAddress}/?bookId={bookId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return false;
        }
    }
}