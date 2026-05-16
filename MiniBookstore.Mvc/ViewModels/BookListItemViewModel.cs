namespace MiniBookstore.Mvc.ViewModels;

public class BookListItemViewModel
{
    public int Id { get; set; }
    public string Isbn { get; set; } = "";
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string Publisher { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockThreshold { get; set; }

    public string PriceText => $"{Price:N0} VNĐ";

    public decimal InventoryValue => Price * StockQuantity;

    public string InventoryValueText => $"{InventoryValue:N0} VNĐ";

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

    public string StockStatusClass
    {
        get
        {
            if (StockQuantity <= 0)
            {
                return "badge badge-danger";
            }

            if (StockQuantity <= MinStockThreshold)
            {
                return "badge badge-warning";
            }

            return "badge badge-success";
        }
    }
}
