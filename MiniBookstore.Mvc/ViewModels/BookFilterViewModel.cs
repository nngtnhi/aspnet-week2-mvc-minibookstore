namespace MiniBookstore.Mvc.ViewModels;

public class BookFilterViewModel
{
    public int? GenreId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Keyword { get; set; }
    public List<BookListItemViewModel> Books { get; set; } = new();
    public List<GenreOptionViewModel> Genres { get; set; } = new();
}
