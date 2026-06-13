using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task AddItemAsync(SaleItem item);
    Task<List<Sale>> GetHistoryReadOnlyAsync();
    Task<int> CountAsync();
    Task SaveChangesAsync();
}
