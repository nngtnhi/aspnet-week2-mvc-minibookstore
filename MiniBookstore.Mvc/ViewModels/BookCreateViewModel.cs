using System.ComponentModel.DataAnnotations;

namespace MiniBookstore.Mvc.ViewModels;

public class BookCreateViewModel
{
    [Required(ErrorMessage = "Tên sách không được để trống")]
    [StringLength(200, ErrorMessage = "Tên sách không được vượt quá 200 ký tự")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Thể loại không được để trống")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Nhà xuất bản không được để trống")]
    public string Publisher { get; set; } = "";

    [Range(1, 100_000_000, ErrorMessage = "Giá bán phải lớn hơn 0")]
    public decimal Price { get; set; }

    [Range(0, 10_000, ErrorMessage = "Số lượng tồn không được âm")]
    public int StockQuantity { get; set; }

    public List<GenreOptionViewModel> Genres { get; set; } = new();
}

public class GenreOptionViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
