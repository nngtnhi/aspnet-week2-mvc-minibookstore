using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public interface IGenreService
{
    Task<List<GenreListItemViewModel>> GetGenreListAsync();
    Task<List<GenreRelationshipViewModel>> GetGenreRelationshipsAsync();
}
