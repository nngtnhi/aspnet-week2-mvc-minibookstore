namespace MiniBookstore.Mvc.ViewModels;

public class BookDetailViewModel
{
    public int Id { get; set; }
    public string Isbn { get; set; } = "";
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string Publisher { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockThreshold { get; set; }
    public DateTime LastRestockedAt { get; set; }

    public string PriceText => $"{Price:N0} VNĐ";

    public decimal InventoryValue => Price * StockQuantity;

    public string InventoryValueText => $"{InventoryValue:N0} VNĐ";

    public string LastRestockedText => LastRestockedAt.ToString("dd/MM/yyyy HH:mm");

    public string StockStatus
    {
        get
        {
            if (StockQuantity <= 0)
            {
                return "Hết sách";
            }

            if (StockQuantity <= MinStockThreshold)
            {
                return "Cần nhập thêm";
            }

            if (StockQuantity >= 20)
            {
                return "Tồn kho cao";
            }

            return "Còn sách";
        }
    }

    public string ReorderSuggestion
    {
        get
        {
            if (StockQuantity <= 0)
            {
                return "Cần nhập hàng ngay vì sách đã hết.";
            }

            if (StockQuantity <= MinStockThreshold)
            {
                return $"Nên nhập thêm. Tồn kho hiện tại chỉ còn {StockQuantity}, mức tối thiểu là {MinStockThreshold}.";
            }

            return "Tồn kho đang ổn định, chưa cần nhập thêm.";
        }
    }
}
