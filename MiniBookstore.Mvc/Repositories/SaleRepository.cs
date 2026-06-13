using Microsoft.EntityFrameworkCore;
using MiniBookstore.Mvc.Data;
using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale)
        => await _context.Sales.AddAsync(sale);

    public async Task AddItemAsync(SaleItem item)
        => await _context.SaleItems.AddAsync(item);

    public Task<List<Sale>> GetHistoryReadOnlyAsync()
        => _context.Sales
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Book)
            .AsNoTracking()
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

    public Task<int> CountAsync()
        => _context.Sales.CountAsync();

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
