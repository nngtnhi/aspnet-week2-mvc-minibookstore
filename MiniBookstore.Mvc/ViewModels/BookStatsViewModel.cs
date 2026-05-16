namespace MiniBookstore.Mvc.ViewModels;

public class BookStatsViewModel
{
    public int TotalTitles { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalInventoryValue { get; set; }
    public int OutOfStockCount { get; set; }
    public int NeedReorderCount { get; set; }

    public string TotalInventoryValueText => $"{TotalInventoryValue:N0} VNĐ";
}
