namespace MiniBookstore.Mvc.ViewModels;

public class GenreIndexViewModel
{
    public List<GenreListItemViewModel> Genres { get; set; } = new();
    public List<GenreRelationshipViewModel> Relationships { get; set; } = new();
}
