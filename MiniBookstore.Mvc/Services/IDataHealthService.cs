using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public interface IDataHealthService
{
    Task<DataHealthViewModel> GetHealthAsync();
}
