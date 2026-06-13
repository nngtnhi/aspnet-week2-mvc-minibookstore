using System.ComponentModel.DataAnnotations;

namespace MiniBookstore.Mvc.ViewModels;

public class SaleCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
    [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn sách")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn sách")]
    public int BookId { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000")]
    public int Quantity { get; set; } = 1;

    public List<BookOptionViewModel> Books { get; set; } = new();
}

public class BookOptionViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public string DisplayText => $"{Title} - Còn {StockQuantity} cuốn - {Price:N0} VNĐ";
}
