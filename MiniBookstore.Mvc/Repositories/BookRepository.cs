using Microsoft.EntityFrameworkCore;
using MiniBookstore.Mvc.Data;
using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Book>> GetAllReadOnlyAsync()
        => _context.Books
            .Include(b => b.Genre)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

    public Task<List<Book>> FilterReadOnlyAsync(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword)
    {
        var query = _context.Books
            .Include(b => b.Genre)
            .AsNoTracking()
            .AsQueryable();

        if (genreId.HasValue)
        {
            query = query.Where(b => b.GenreId == genreId.Value);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(b => b.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(b => b.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(b =>
                b.Title.Contains(keyword) ||
                b.Isbn.Contains(keyword) ||
                (b.Genre != null && b.Genre.Name.Contains(keyword)));
        }

        return query.OrderBy(b => b.Title).ToListAsync();
    }

    public Task<Book?> GetByIdReadOnlyAsync(int id)
        => _context.Books
            .Include(b => b.Genre)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

    public Task<Book?> GetByIdTrackedAsync(int id)
        => _context.Books.FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Book book)
        => await _context.Books.AddAsync(book);

    public Task<int> CountAsync()
        => _context.Books.CountAsync();

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
