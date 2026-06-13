using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public interface IBookService
{
    Task<List<BookListItemViewModel>> GetBookListAsync();
    Task<BookDetailViewModel?> GetDetailAsync(int id);
    Task<List<BookListItemViewModel>> FilterBooksAsync(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword);
    Task<List<BookListItemViewModel>> SearchAsync(string? keyword, decimal? minPrice);
    Task<BookStatsViewModel> GetStatsAsync();
    Task CreateBookAsync(BookCreateViewModel model);
}
