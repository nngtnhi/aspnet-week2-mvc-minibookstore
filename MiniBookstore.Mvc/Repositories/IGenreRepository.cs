using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllWithBookCountReadOnlyAsync();
    Task<List<Genre>> GetAllReadOnlyAsync();
    Task<Genre?> GetByIdReadOnlyAsync(int id);
}
