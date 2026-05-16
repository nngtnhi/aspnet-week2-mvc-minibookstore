using Microsoft.AspNetCore.Mvc;
using MiniBookstore.Mvc.Models;
using MiniBookstore.Mvc.Services;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetAll()
            .Select(ToListItemViewModel)
            .ToList();

        return View(books);
    }

    public IActionResult Detail(int id)
    {
        var book = _bookService.GetById(id);

        if (book == null)
        {
            return NotFound($"Không tìm thấy cuốn sách nào có ID = {id}");
        }

        var viewModel = ToDetailViewModel(book);

        return View(viewModel);
    }

    public IActionResult Stats()
    {
        var stats = _bookService.GetStats();

        return View(stats);
    }

    public IActionResult Welcome()
    {
        return Content("Chào mừng đến với hệ thống quản lý Kho Sách - MVC Lab02");
    }

    public IActionResult BookJson()
    {
        var books = _bookService.GetAll()
            .Select(book => new
            {
                book.Id,
                book.Isbn,
                book.Title,
                book.Category,
                book.Publisher,
                book.Price,
                book.StockQuantity
            });

        return Json(books);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là response 404 demo từ action Force404.");
    }

    public IActionResult CategoryInfo()
    {
        return Content("Danh mục hiện có: Công nghệ thông tin, Kỹ năng sống, Khoa học, Văn học");
    }

    private static BookListItemViewModel ToListItemViewModel(Book book)
    {
        return new BookListItemViewModel
        {
            Id = book.Id,
            Isbn = book.Isbn,
            Title = book.Title,
            Category = book.Category,
            Publisher = book.Publisher,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            MinStockThreshold = book.MinStockThreshold
        };
    }

    private static BookDetailViewModel ToDetailViewModel(Book book)
    {
        return new BookDetailViewModel
        {
            Id = book.Id,
            Isbn = book.Isbn,
            Title = book.Title,
            Category = book.Category,
            Publisher = book.Publisher,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            MinStockThreshold = book.MinStockThreshold,
            LastRestockedAt = book.LastRestockedAt
        };
    }
}
