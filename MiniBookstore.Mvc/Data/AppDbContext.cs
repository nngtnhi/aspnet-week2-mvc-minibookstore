using Microsoft.EntityFrameworkCore;
using MiniBookstore.Mvc.Models;

namespace MiniBookstore.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.BookCode).IsRequired().HasMaxLength(20);
            entity.Property(b => b.Isbn).IsRequired().HasMaxLength(20);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Publisher).IsRequired().HasMaxLength(150);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sales");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("SaleItems");
            entity.HasKey(si => si.Id);
            entity.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId);
            entity.HasOne(si => si.Book)
                .WithMany()
                .HasForeignKey(si => si.BookId);
        });

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Công nghệ thông tin" },
            new Genre { Id = 2, Name = "Kỹ năng sống" },
            new Genre { Id = 3, Name = "Khoa học" },
            new Genre { Id = 4, Name = "Văn học" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, BookCode = "BK001", Isbn = "ISBN-978-01", Title = "C# Programming Fundamentals", GenreId = 1, Publisher = "NXB Trẻ", Price = 150000, StockQuantity = 25 },
            new Book { Id = 2, BookCode = "BK002", Isbn = "ISBN-978-02", Title = "Đắc Nhân Tâm", GenreId = 2, Publisher = "NXB Tổng Hợp", Price = 95000, StockQuantity = 5 },
            new Book { Id = 3, BookCode = "BK003", Isbn = "ISBN-978-03", Title = "Clean Code", GenreId = 1, Publisher = "Prentice Hall", Price = 450000, StockQuantity = 3 },
            new Book { Id = 4, BookCode = "BK004", Isbn = "ISBN-978-04", Title = "Sapiens - Lược Sử Loài Người", GenreId = 3, Publisher = "NXB Tri Thức", Price = 200000, StockQuantity = 15 },
            new Book { Id = 5, BookCode = "BK005", Isbn = "ISBN-978-05", Title = "Design Patterns", GenreId = 1, Publisher = "O'Reilly Media", Price = 520000, StockQuantity = 3 },
            new Book { Id = 6, BookCode = "BK006", Isbn = "ISBN-978-06", Title = "Nhà Giả Kim", GenreId = 4, Publisher = "NXB Văn Học", Price = 75000, StockQuantity = 30 },
            new Book { Id = 7, BookCode = "BK007", Isbn = "ISBN-978-07", Title = "ASP.NET Core in Action", GenreId = 1, Publisher = "Manning Publications", Price = 680000, StockQuantity = 8 },
            new Book { Id = 8, BookCode = "BK008", Isbn = "ISBN-978-08", Title = "Tuổi Trẻ Đáng Giá Bao Nhiêu", GenreId = 2, Publisher = "NXB Hội Nhà Văn", Price = 85000, StockQuantity = 2 }
        );
    }
}
