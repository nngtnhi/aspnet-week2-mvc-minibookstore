namespace MiniBookstore.Mvc.ViewModels;

public class GenreRelationshipViewModel
{
    public int Id { get; set; }
    public string GenreName { get; set; } = string.Empty;
    public List<string> BookTitles { get; set; } = new();
    public string Relationship { get; set; } = "1 - Many";
    public string DbSet { get; set; } = "Genres";
}
