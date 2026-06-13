namespace MiniBookstore.Mvc.ViewModels;

public class SaleHistoryViewModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleHistoryItemViewModel> Items { get; set; } = new();

    public string CreatedAtText => CreatedAt.ToString("dd/MM/yyyy HH:mm");
    public string TotalAmountText => $"{TotalAmount:N0} VNĐ";
}

public class SaleHistoryItemViewModel
{
    public string BookTitle { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public string UnitPriceText => $"{UnitPrice:N0} VNĐ";
    public decimal LineTotal => UnitPrice * Quantity;
    public string LineTotalText => $"{LineTotal:N0} VNĐ";
}
