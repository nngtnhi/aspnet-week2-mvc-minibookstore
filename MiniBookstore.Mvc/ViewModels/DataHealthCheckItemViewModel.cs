namespace MiniBookstore.Mvc.ViewModels;

public class DataHealthCheckItemViewModel
{
    public string Check { get; set; } = string.Empty;
    public string Expected { get; set; } = string.Empty;
    public string Actual { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}
