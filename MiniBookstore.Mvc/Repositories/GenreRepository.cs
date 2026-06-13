using Microsoft.EntityFrameworkCore;
using MiniBookstore.Mvc.Data;
using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Genre>> GetAllWithBookCountReadOnlyAsync()
        => _context.Genres
            .Include(g => g.Books)
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ToListAsync();

    public Task<List<Genre>> GetAllReadOnlyAsync()
        => _context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ToListAsync();

    public Task<Genre?> GetByIdReadOnlyAsync(int id)
        => _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
}
