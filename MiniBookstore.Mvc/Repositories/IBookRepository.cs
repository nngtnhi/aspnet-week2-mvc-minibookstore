using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllReadOnlyAsync();
    Task<List<Book>> FilterReadOnlyAsync(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword);
    Task<Book?> GetByIdReadOnlyAsync(int id);
    Task<Book?> GetByIdTrackedAsync(int id);
    Task AddAsync(Book book);
    Task<int> CountAsync();
    Task SaveChangesAsync();
}
