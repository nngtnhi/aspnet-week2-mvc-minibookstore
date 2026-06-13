using Microsoft.AspNetCore.Mvc;
using MiniBookstore.Mvc.Repositories;
using MiniBookstore.Mvc.Services;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Controllers;

public class SalesController : Controller
{
    private readonly ISaleService _saleService;
    private readonly IBookRepository _bookRepository;

    public SalesController(ISaleService saleService, IBookRepository bookRepository)
    {
        _saleService = saleService;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();

        var viewModel = new SaleCreateViewModel
        {
            Quantity = 1,
            Books = books.Select(b => new BookOptionViewModel
            {
                Id = b.Id,
                Title = b.Title,
                StockQuantity = b.StockQuantity,
                Price = b.Price
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaleCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadBooksAsync(model);
            return View(model);
        }

        try
        {
            await _saleService.CreateSaleAsync(model);
            TempData["SuccessMessage"] = "Tạo đơn bán sách thành công.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadBooksAsync(model);
            return View(model);
        }
    }

    public async Task<IActionResult> Index()
    {
        var history = await _saleService.GetHistoryAsync();
        return View(history);
    }

    private async Task LoadBooksAsync(SaleCreateViewModel model)
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        model.Books = books.Select(b => new BookOptionViewModel
        {
            Id = b.Id,
            Title = b.Title,
            StockQuantity = b.StockQuantity,
            Price = b.Price
        }).ToList();
    }
}
