using MiniBookstore.Mvc.Models;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public class BookService
{
    private readonly List<Book> _books = new()
    {
        new Book
        {
            Id = 1,
            Isbn = "ISBN-978-01",
            Title = "C# Programming Fundamentals",
            Category = "Công nghệ thông tin",
            Publisher = "NXB Trẻ",
            Price = 150000,
            StockQuantity = 25,
            MinStockThreshold = 10,
            LastRestockedAt = new DateTime(2025, 5, 9, 9, 0, 0)
        },
        new Book
        {
            Id = 2,
            Isbn = "ISBN-978-02",
            Title = "Đắc Nhân Tâm",
            Category = "Kỹ năng sống",
            Publisher = "NXB Tổng Hợp",
            Price = 95000,
            StockQuantity = 5,
            MinStockThreshold = 10,
            LastRestockedAt = new DateTime(2025, 4, 20, 14, 30, 0)
        },
        new Book
        {
            Id = 3,
            Isbn = "ISBN-978-03",
            Title = "Clean Code",
            Category = "Công nghệ thông tin",
            Publisher = "Prentice Hall",
            Price = 450000,
            StockQuantity = 0,
            MinStockThreshold = 5,
            LastRestockedAt = new DateTime(2025, 3, 15, 8, 0, 0)
        },
        new Book
        {
            Id = 4,
            Isbn = "ISBN-978-04",
            Title = "Sapiens - Lược Sử Loài Người",
            Category = "Khoa học",
            Publisher = "NXB Tri Thức",
            Price = 200000,
            StockQuantity = 15,
            MinStockThreshold = 5,
            LastRestockedAt = new DateTime(2025, 5, 5, 10, 15, 0)
        },
        new Book
        {
            Id = 5,
            Isbn = "ISBN-978-05",
            Title = "Design Patterns",
            Category = "Công nghệ thông tin",
            Publisher = "O'Reilly Media",
            Price = 520000,
            StockQuantity = 3,
            MinStockThreshold = 5,
            LastRestockedAt = new DateTime(2025, 4, 10, 16, 0, 0)
        },
        new Book
        {
            Id = 6,
            Isbn = "ISBN-978-06",
            Title = "Nhà Giả Kim",
            Category = "Văn học",
            Publisher = "NXB Văn Học",
            Price = 75000,
            StockQuantity = 30,
            MinStockThreshold = 8,
            LastRestockedAt = new DateTime(2025, 5, 8, 11, 0, 0)
        },
        new Book
        {
            Id = 7,
            Isbn = "ISBN-978-07",
            Title = "ASP.NET Core in Action",
            Category = "Công nghệ thông tin",
            Publisher = "Manning Publications",
            Price = 680000,
            StockQuantity = 8,
            MinStockThreshold = 4,
            LastRestockedAt = new DateTime(2025, 5, 1, 9, 30, 0)
        },
        new Book
        {
            Id = 8,
            Isbn = "ISBN-978-08",
            Title = "Tuổi Trẻ Đáng Giá Bao Nhiêu",
            Category = "Kỹ năng sống",
            Publisher = "NXB Hội Nhà Văn",
            Price = 85000,
            StockQuantity = 0,
            MinStockThreshold = 10,
            LastRestockedAt = new DateTime(2025, 2, 28, 7, 45, 0)
        }
    };

    public List<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public BookStatsViewModel GetStats()
    {
        var totalTitles = _books.Count;

        var totalQuantity = _books.Sum(b => b.StockQuantity);

        var totalInventoryValue = _books.Sum(b => b.Price * b.StockQuantity);

        var outOfStockCount = _books.Count(b => b.StockQuantity <= 0);

        var needReorderCount = _books.Count(b =>
            b.StockQuantity > 0 && b.StockQuantity <= b.MinStockThreshold);

        return new BookStatsViewModel
        {
            TotalTitles = totalTitles,
            TotalQuantity = totalQuantity,
            TotalInventoryValue = totalInventoryValue,
            OutOfStockCount = outOfStockCount,
            NeedReorderCount = needReorderCount
        };
    }
}
