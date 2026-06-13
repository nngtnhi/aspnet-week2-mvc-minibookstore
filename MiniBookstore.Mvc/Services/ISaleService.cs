using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public interface ISaleService
{
    Task CreateSaleAsync(SaleCreateViewModel model);
    Task<List<SaleHistoryViewModel>> GetHistoryAsync();
}
