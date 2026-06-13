using Microsoft.EntityFrameworkCore;
using MiniBookstore.Mvc.Data;
using MiniBookstore.Mvc.Models;
using MiniBookstore.Mvc.Repositories;
using MiniBookstore.Mvc.ViewModels;

namespace MiniBookstore.Mvc.Services;

public class SaleService : ISaleService
{
    private readonly AppDbContext _context;
    private readonly IBookRepository _bookRepository;
    private readonly ISaleRepository _saleRepository;

    public SaleService(
        AppDbContext context,
        IBookRepository bookRepository,
        ISaleRepository saleRepository)
    {
        _context = context;
        _bookRepository = bookRepository;
        _saleRepository = saleRepository;
    }

    public async Task CreateSaleAsync(SaleCreateViewModel model)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var book = await _bookRepository.GetByIdTrackedAsync(model.BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Không tìm thấy sách.");
            }

            if (book.StockQuantity < model.Quantity)
            {
                throw new InvalidOperationException($"Không đủ tồn kho. Hiện còn {book.StockQuantity} cuốn.");
            }

            var sale = new Sale
            {
                CustomerName = model.CustomerName,
                CreatedAt = DateTime.Now,
                TotalAmount = book.Price * model.Quantity
            };

            await _saleRepository.AddAsync(sale);
            await _saleRepository.SaveChangesAsync();

            var item = new SaleItem
            {
                SaleId = sale.Id,
                BookId = book.Id,
                Quantity = model.Quantity,
                UnitPrice = book.Price
            };

            await _saleRepository.AddItemAsync(item);
            book.StockQuantity -= model.Quantity;

            await _bookRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<SaleHistoryViewModel>> GetHistoryAsync()
    {
        var sales = await _saleRepository.GetHistoryReadOnlyAsync();

        return sales.Select(s => new SaleHistoryViewModel
        {
            Id = s.Id,
            CustomerName = s.CustomerName,
            CreatedAt = s.CreatedAt,
            TotalAmount = s.TotalAmount,
            Items = s.SaleItems.Select(si => new SaleHistoryItemViewModel
            {
                BookTitle = si.Book?.Title ?? "N/A",
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice
            }).ToList()
        }).ToList();
    }
}
