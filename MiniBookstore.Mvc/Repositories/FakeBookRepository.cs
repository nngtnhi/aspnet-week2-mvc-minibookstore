using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public class FakeBookRepository : IBookRepository
{
    public Task<List<Book>> GetAllReadOnlyAsync()
    {
        var data = new List<Book>
        {
            new()
            {
                Id = 1,
                Title = "Sách test",
                Isbn = "ISBN-FAKE",
                Price = 100000,
                StockQuantity = 2,
                Genre = new Genre { Name = "Test" }
            }
        };

        return Task.FromResult(data);
    }

    public Task<List<Book>> FilterReadOnlyAsync(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword)
        => GetAllReadOnlyAsync();

    public Task<Book?> GetByIdReadOnlyAsync(int id)
        => Task.FromResult<Book?>(new Book { Id = id, Title = "Sách test", Price = 100000, StockQuantity = 2 });

    public Task<Book?> GetByIdTrackedAsync(int id)
        => GetByIdReadOnlyAsync(id);

    public Task AddAsync(Book book) => Task.CompletedTask;

    public Task<int> CountAsync() => Task.FromResult(1);

    public Task SaveChangesAsync() => Task.CompletedTask;
}
