using Microsoft.AspNetCore.Mvc;
using MiniBookstore.Mvc.Repositories;
using MiniBookstore.Mvc.Services;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IGenreRepository _genreRepository;

    public BooksController(IBookService bookService, IGenreRepository genreRepository)
    {
        _bookService = bookService;
        _genreRepository = genreRepository;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBookListAsync();
        return View(books);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var book = await _bookService.GetDetailAsync(id);

        if (book == null)
        {
            return NotFound($"Không tìm thấy cuốn sách nào có ID = {id}");
        }

        return View(book);
    }

    public async Task<IActionResult> Stats()
    {
        var stats = await _bookService.GetStatsAsync();
        return View(stats);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string? keyword, decimal? minPrice)
    {
        var books = await _bookService.SearchAsync(keyword, minPrice);

        var viewModel = new BookSearchViewModel
        {
            Keyword = keyword ?? "",
            MinPrice = minPrice,
            Books = books
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword)
    {
        var genres = await _genreRepository.GetAllReadOnlyAsync();
        var books = await _bookService.FilterBooksAsync(genreId, minPrice, maxPrice, keyword);

        var viewModel = new BookFilterViewModel
        {
            GenreId = genreId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Keyword = keyword,
            Books = books,
            Genres = genres.Select(g => new GenreOptionViewModel { Id = g.Id, Name = g.Name }).ToList()
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var genres = await _genreRepository.GetAllReadOnlyAsync();

        var viewModel = new BookCreateViewModel
        {
            StockQuantity = 1,
            Genres = genres.Select(g => new GenreOptionViewModel { Id = g.Id, Name = g.Name }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var genres = await _genreRepository.GetAllReadOnlyAsync();
            model.Genres = genres.Select(g => new GenreOptionViewModel { Id = g.Id, Name = g.Name }).ToList();
            return View(model);
        }

        await _bookService.CreateBookAsync(model);

        TempData["SuccessMessage"] = "Đã thêm sách thành công.";

        return RedirectToAction(nameof(Index));
    }
}
