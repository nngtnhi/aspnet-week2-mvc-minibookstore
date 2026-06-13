using Microsoft.Extensions.Options;
using MiniBookstore.Mvc.Models;
using MiniBookstore.Mvc.Options;
using MiniBookstore.Mvc.Repositories;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly AppSettings _settings;

    public BookService(IBookRepository bookRepository, IOptions<AppSettings> options)
    {
        _bookRepository = bookRepository;
        _settings = options.Value;
    }

    public async Task<List<BookListItemViewModel>> GetBookListAsync()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        return books.Select(MapToListItem).ToList();
    }

    public async Task<BookDetailViewModel?> GetDetailAsync(int id)
    {
        var book = await _bookRepository.GetByIdReadOnlyAsync(id);
        return book == null ? null : MapToDetail(book);
    }

    public async Task<List<BookListItemViewModel>> FilterBooksAsync(int? genreId, decimal? minPrice, decimal? maxPrice, string? keyword)
    {
        var books = await _bookRepository.FilterReadOnlyAsync(genreId, minPrice, maxPrice, keyword);
        return books.Select(MapToListItem).ToList();
    }

    public async Task<List<BookListItemViewModel>> SearchAsync(string? keyword, decimal? minPrice)
    {
        return await FilterBooksAsync(null, minPrice, null, keyword);
    }

    public async Task<BookStatsViewModel> GetStatsAsync()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        var threshold = _settings.LowStockThreshold;

        return new BookStatsViewModel
        {
            TotalTitles = books.Count,
            TotalQuantity = books.Sum(b => b.StockQuantity),
            TotalInventoryValue = books.Sum(b => b.Price * b.StockQuantity),
            OutOfStockCount = books.Count(b => b.StockQuantity <= 0),
            NeedReorderCount = books.Count(b => b.StockQuantity > 0 && b.StockQuantity <= threshold),
            LowStockThreshold = threshold
        };
    }

    public async Task CreateBookAsync(BookCreateViewModel model)
    {
        var book = new Book
        {
            BookCode = $"BK-{DateTime.Now:yyyyMMddHHmmss}",
            Isbn = $"ISBN-NEW-{DateTime.Now:yyyyMMddHHmmss}",
            Title = model.Title,
            Publisher = model.Publisher,
            Price = model.Price,
            StockQuantity = model.StockQuantity,
            GenreId = model.GenreId
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();
    }

    private BookListItemViewModel MapToListItem(Book book)
    {
        var threshold = _settings.LowStockThreshold;

        return new BookListItemViewModel
        {
            Id = book.Id,
            BookCode = book.BookCode,
            Isbn = book.Isbn,
            Title = book.Title,
            GenreName = book.Genre?.Name ?? "N/A",
            Publisher = book.Publisher,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            LowStockThreshold = threshold
        };
    }

    private BookDetailViewModel MapToDetail(Book book)
    {
        return new BookDetailViewModel
        {
            Id = book.Id,
            BookCode = book.BookCode,
            Isbn = book.Isbn,
            Title = book.Title,
            GenreName = book.Genre?.Name ?? "N/A",
            Publisher = book.Publisher,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            LowStockThreshold = _settings.LowStockThreshold
        };
    }
}
