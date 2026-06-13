using MiniBookstore.Mvc.Repositories;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<List<GenreListItemViewModel>> GetGenreListAsync()
    {
        var genres = await _genreRepository.GetAllWithBookCountReadOnlyAsync();

        return genres.Select(g => new GenreListItemViewModel
        {
            Id = g.Id,
            Name = g.Name,
            BookCount = g.Books.Count
        }).ToList();
    }

    public async Task<List<GenreRelationshipViewModel>> GetGenreRelationshipsAsync()
    {
        var genres = await _genreRepository.GetAllWithBookCountReadOnlyAsync();

        return genres.Select(g => new GenreRelationshipViewModel
        {
            Id = g.Id,
            GenreName = g.Name,
            BookTitles = g.Books.Select(b => b.Title).ToList(),
            Relationship = "1 - Many",
            DbSet = "Genres"
        }).ToList();
    }
}
