namespace MiniBookstore.Mvc.Models;

public class Book
{
    public int Id { get; set; }
    public string BookCode { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
}
