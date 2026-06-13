using Microsoft.AspNetCore.Mvc;
using MiniBookstore.Mvc.Services;

namespace MiniBookstore.Mvc.Controllers;

public class DataHealthController : Controller
{
    private readonly IDataHealthService _dataHealthService;

    public DataHealthController(IDataHealthService dataHealthService)
    {
        _dataHealthService = dataHealthService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _dataHealthService.GetHealthAsync();
        return View(model);
    }
}
