**Lab04 — DI, Service/Repository, Options Pattern & EF Core Data Layer**

# **1\) Mục tiêu bài Lab**

* Hiểu và áp dụng Dependency Injection trong ASP.NET Core MVC.  
* Phân biệt Transient, Scoped, Singleton và chọn lifetime phù hợp cho service, repository, DbContext.  
* Tách Controller khỏi data logic bằng Service/Repository Pattern.  
* Dùng Options Pattern để cấu hình appsettings.json thành class strongly-typed.  
* Xây dựng minimal test mindset: code dễ thay dependency, dễ kiểm tra logic.  
* Tạo AppDbContext, Entity, Relationship cơ bản bằng EF Core.  
* Tạo migration, update database và seed data.  
* Phân biệt Tracking và AsNoTracking khi đọc/sửa dữ liệu.  
* Dùng transaction cơ bản khi tạo đơn hàng và trừ tồn kho.  
* Quản lý code bằng Git và nộp bài qua GitHub.

# **2\) Các bước tiến hành**

* Mở lại project Lab03.  
* Tạo cấu trúc thư mục Models / Data / Repositories / Services / Options / ViewModels.  
* Tạo entity Category, Product, Customer, Order, OrderItem.  
* Tạo AppDbContext và đăng ký DbContext bằng DI.  
* Tạo AppSettings và bind từ appsettings.json bằng Options Pattern.  
* Tạo interface và implementation cho Repository.  
* Tạo Service xử lý nghiệp vụ sản phẩm và đơn hàng.  
* Cập nhật Controller để chỉ gọi Service.  
* Tạo Migration InitialCreate và cập nhật database.  
* Seed dữ liệu mẫu.  
* Tạo trang Products dùng Repository \+ EF Core.  
* Tạo trang Categories minh họa quan hệ Category \- Product.  
* Tạo trang DataHealth minh họa migration, seed, tracking/no-tracking.  
* Tạo flow Create Order có transaction.  
* Chạy chương trình, kiểm tra URL, commit code và nộp bài.

# **3\) Mô tả bài toán**

Tên bài toán: Mini Shop Data Layer Upgrade

Bối cảnh: Ở Lab03, project đã có Layout, Partial View, Tag Helpers, Search và Create Form. Tuy nhiên dữ liệu vẫn còn đơn giản, service chưa tách rõ repository, config chưa được gom thành strongly-typed object, và chưa có database thật. Trong Lab04, học viên nâng cấp project thành một MVC app có data layer rõ ràng bằng EF Core.

| Vấn đề từ Lab03 | Nâng cấp trong Lab04 |
| :---- | :---- |
| Dữ liệu còn fake/in-memory | Chuyển sang EF Core DbContext \+ database |
| Controller/Service dễ ôm logic | Tách Controller → Service → Repository → DbContext |
| Config rải rác | Dùng Options Pattern |
| Chưa có relationship | Tạo Category \- Product, Customer \- Order, Order \- OrderItem |
| Chưa quản lý thay đổi DB | Dùng Migration \+ Seed Data |
| Chưa có nghiệp vụ nhiều bước | Tạo Order bằng transaction |

# **4\) Yêu cầu chức năng**

1. GET / để xem trang chủ.  
2. GET /Products để xem danh sách sản phẩm lấy từ database.  
3. GET /Products/Detail/1 để xem chi tiết sản phẩm.  
4. GET /Categories để xem danh mục và số lượng sản phẩm theo danh mục.  
5. GET /DataHealth để xem thông tin kiểm tra dữ liệu: migration, seed, tracking/no-tracking.  
6. GET /Orders/Create để mở form tạo đơn hàng.  
7. POST /Orders/Create để tạo đơn hàng, thêm OrderItem và trừ Stock bằng transaction.  
8. Nếu tạo đơn hàng thành công, redirect về /Orders/Success hoặc /Products và hiển thị thông báo.  
9. Nếu lỗi tồn kho hoặc lỗi ghi dữ liệu, rollback transaction và hiển thị thông báo lỗi.

# **5\) Giao diện mong đợi sau Lab04**

## **5.1 Trang Products sau khi dùng EF Core \+ Service/Repository**

Khi vào /Products, danh sách sản phẩm được lấy qua luồng Controller → Service → Repository → DbContext → Database. Mỗi sản phẩm vẫn có thể dùng Product Card từ Lab03, nhưng dữ liệu không còn hardcode trong controller.

![][image1]

## **5.2 Trang Categories minh họa relationship**

Trang /Categories cho thấy quan hệ One-to-Many: một Category có nhiều Product. Đây là phần kiểm tra entity mapping và relationship cơ bản.

![][image2]

## **5.3 Trang Create Order có transaction**

Trang /Orders/Create giúp học viên thấy một nghiệp vụ ghi nhiều bảng: tạo Order, tạo OrderItem, trừ Stock. Nếu một bước lỗi thì rollback toàn bộ.

![][image3]

## **5.4 Trang Data Health kiểm tra EF Core**

Trang /DataHealth là màn hình kiểm tra nhanh: database đã có migration, seed data đã có dữ liệu, query list dùng AsNoTracking, nghiệp vụ order có transaction.

![][image4]

# **6\) Mở lại project Lab03**

* Mở thư mục project Lab03 trong VS Code.  
* Chạy dotnet run và kiểm tra /Products, /Products/Search, /Products/Create còn hoạt động.  
* Tạo branch mới: lab04-data-layer.

cd AspNetWeek2.Mvc  
dotnet run  
git checkout \-b lab04-data-layer

# **7\) Cấu trúc dự án sau Lab04**

AspNetWeek2.Mvc  
├── Controllers  
│   ├── ProductsController.cs  
│   ├── CategoriesController.cs  
│   ├── OrdersController.cs  
│   └── DataHealthController.cs  
├── Data  
│   └── AppDbContext.cs  
├── Models  
│   ├── Category.cs  
│   ├── Product.cs  
│   ├── Customer.cs  
│   ├── Order.cs  
│   └── OrderItem.cs  
├── Options  
│   └── AppSettings.cs  
├── Repositories  
│   ├── IProductRepository.cs  
│   ├── ProductRepository.cs  
│   ├── IOrderRepository.cs  
│   └── OrderRepository.cs  
├── Services  
│   ├── IProductService.cs  
│   ├── ProductService.cs  
│   ├── IOrderService.cs  
│   └── OrderService.cs  
├── ViewModels  
│   ├── ProductListItemViewModel.cs  
│   ├── OrderCreateViewModel.cs  
│   └── DataHealthViewModel.cs  
├── Views  
│   ├── Products  
│   ├── Categories  
│   ├── Orders  
│   └── DataHealth  
└── Program.cs

# **8\) Cài package EF Core**

* Cài các package cần thiết cho SQL Server và EF Core tools.  
* Nếu dùng SQLite để lab nhẹ hơn, học viên có thể thay package SQL Server bằng SQLite.

dotnet add package Microsoft.EntityFrameworkCore  
dotnet add package Microsoft.EntityFrameworkCore.SqlServer  
dotnet add package Microsoft.EntityFrameworkCore.Design  
dotnet tool install \--global dotnet-ef

# **9\) Tạo Entity và Relationship**

Tạo các entity chính cho case Mini Shop. Trọng tâm là hiểu Category \- Product là One-to-Many, Customer \- Order là One-to-Many, Order \- OrderItem là One-to-Many, Product \- OrderItem là Many-to-One.

// Models/Category.cs  
public class Category  
{  
    public int Id { get; set; }  
    public string Name { get; set; } \= string.Empty;  
    public ICollection\<Product\> Products { get; set; } \= new List\<Product\>();  
}

// Models/Product.cs  
public class Product  
{  
    public int Id { get; set; }  
    public string Name { get; set; } \= string.Empty;  
    public decimal Price { get; set; }  
    public int Stock { get; set; }  
    public int CategoryId { get; set; }  
    public Category? Category { get; set; }  
}

// Models/Order.cs  
public class Order  
{  
    public int Id { get; set; }  
    public DateTime CreatedAt { get; set; } \= DateTime.Now;  
    public decimal TotalAmount { get; set; }  
    public ICollection\<OrderItem\> OrderItems { get; set; } \= new List\<OrderItem\>();  
}

# **10\) Tạo AppDbContext và Mapping**

// Data/AppDbContext.cs  
using Microsoft.EntityFrameworkCore;  
using AspNetWeek2.Mvc.Models;

public class AppDbContext : DbContext  
{  
    public AppDbContext(DbContextOptions\<AppDbContext\> options) : base(options) { }

    public DbSet\<Category\> Categories \=\> Set\<Category\>();  
    public DbSet\<Product\> Products \=\> Set\<Product\>();  
    public DbSet\<Order\> Orders \=\> Set\<Order\>();  
    public DbSet\<OrderItem\> OrderItems \=\> Set\<OrderItem\>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {  
        modelBuilder.Entity\<Category\>(entity \=\>  
        {  
            entity.ToTable("Categories");  
            entity.HasKey(c \=\> c.Id);  
            entity.Property(c \=\> c.Name).IsRequired().HasMaxLength(100);  
        });

        modelBuilder.Entity\<Product\>(entity \=\>  
        {  
            entity.ToTable("Products");  
            entity.HasKey(p \=\> p.Id);  
            entity.Property(p \=\> p.Name).IsRequired().HasMaxLength(150);  
            entity.Property(p \=\> p.Price).HasColumnType("decimal(18,2)");  
            entity.HasOne(p \=\> p.Category)  
                  .WithMany(c \=\> c.Products)  
                  .HasForeignKey(p \=\> p.CategoryId);  
        });  
    }  
}

# **11\) Cấu hình appsettings.json và Options Pattern**

Thay vì đọc config bằng string key rải rác, tạo class AppSettings để gom các cấu hình có ý nghĩa.

// appsettings.json  
{  
  "ConnectionStrings": {  
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MiniShopLab04;Trusted\_Connection=True;MultipleActiveResultSets=true"  
  },  
  "AppSettings": {  
    "AppName": "Mini Shop Lab04",  
    "SupportEmail": "support@vtca.edu.vn",  
    "EnableSeedData": true  
  }  
}

// Options/AppSettings.cs  
public class AppSettings  
{  
    public string AppName { get; set; } \= string.Empty;  
    public string SupportEmail { get; set; } \= string.Empty;  
    public bool EnableSeedData { get; set; }  
}

# **12\) Đăng ký DI trong Program.cs**

var builder \= WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure\<AppSettings\>(  
    builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext\<AppDbContext\>(options \=\>  
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped\<IProductRepository, ProductRepository\>();  
builder.Services.AddScoped\<IProductService, ProductService\>();  
builder.Services.AddScoped\<IOrderRepository, OrderRepository\>();  
builder.Services.AddScoped\<IOrderService, OrderService\>();

var app \= builder.Build();  
app.UseStaticFiles();  
app.UseRouting();  
app.MapDefaultControllerRoute();  
app.Run();

| Service | Lifetime đề xuất | Lý do |
| :---- | :---- | :---- |
| DbContext | Scoped | Mỗi request dùng một context riêng |
| Repository | Scoped | Làm việc theo request và dùng DbContext |
| Business Service | Scoped | Xử lý nghiệp vụ theo request |
| Helper nhỏ không giữ state | Transient | Tạo mới nhanh, ít chi phí |
| Cache/config provider đặc biệt | Singleton | Dùng chung toàn app, phải thread-safe |

# **13\) Tạo Repository Layer**

public interface IProductRepository  
{  
    Task\<List\<Product\>\> GetAllAsync();  
    Task\<List\<Product\>\> GetAllReadOnlyAsync();  
    Task\<Product?\> GetByIdAsync(int id);  
    Task AddAsync(Product product);  
    Task SaveChangesAsync();  
}

public class ProductRepository : IProductRepository  
{  
    private readonly AppDbContext \_context;  
    public ProductRepository(AppDbContext context)  
    {  
        \_context \= context;  
    }

    public Task\<List\<Product\>\> GetAllAsync()  
        \=\> \_context.Products.Include(p \=\> p.Category).ToListAsync();

    public Task\<List\<Product\>\> GetAllReadOnlyAsync()  
        \=\> \_context.Products.Include(p \=\> p.Category).AsNoTracking().ToListAsync();

    public Task\<Product?\> GetByIdAsync(int id)  
        \=\> \_context.Products.Include(p \=\> p.Category).FirstOrDefaultAsync(p \=\> p.Id \== id);

    public async Task AddAsync(Product product)  
        \=\> await \_context.Products.AddAsync(product);

    public Task SaveChangesAsync()  
        \=\> \_context.SaveChangesAsync();  
}

# **14\) Tạo Service Layer**

public interface IProductService  
{  
    Task\<List\<ProductListItemViewModel\>\> GetProductListAsync();  
}

public class ProductService : IProductService  
{  
    private readonly IProductRepository \_productRepository;  
    private readonly AppSettings \_settings;

    public ProductService(IProductRepository productRepository, IOptions\<AppSettings\> options)  
    {  
        \_productRepository \= productRepository;  
        \_settings \= options.Value;  
    }

    public async Task\<List\<ProductListItemViewModel\>\> GetProductListAsync()  
    {  
        var products \= await \_productRepository.GetAllReadOnlyAsync();  
        return products.Select(p \=\> new ProductListItemViewModel  
        {  
            Id \= p.Id,  
            Name \= p.Name,  
            Price \= p.Price,  
            Stock \= p.Stock,  
            CategoryName \= p.Category \!= null ? p.Category.Name : "N/A"  
        }).ToList();  
    }  
}

# **15\) Cập nhật Controller: Controller chỉ gọi Service**

public class ProductsController : Controller  
{  
    private readonly IProductService \_productService;

    public ProductsController(IProductService productService)  
    {  
        \_productService \= productService;  
    }

    public async Task\<IActionResult\> Index()  
    {  
        var products \= await \_productService.GetProductListAsync();  
        return View(products);  
    }  
}

# **16\) Tạo Migration và Update Database**

* Sau khi entity và AppDbContext đã sẵn sàng, tạo migration đầu tiên.  
* Chạy update database để tạo bảng thật.  
* Mở database để kiểm tra bảng Categories, Products, Orders, OrderItems.

dotnet ef migrations add InitialCreate  
dotnet ef database update

| Lệnh | Ý nghĩa |
| :---- | :---- |
| dotnet ef migrations add InitialCreate | Tạo file mô tả thay đổi cấu trúc DB |
| dotnet ef database update | Áp dụng migration xuống database |
| dotnet ef migrations remove | Xóa migration gần nhất nếu chưa update DB |

# **17\) Seed Data**

protected override void OnModelCreating(ModelBuilder modelBuilder)  
{  
    modelBuilder.Entity\<Category\>().HasData(  
        new Category { Id \= 1, Name \= "Accessories" },  
        new Category { Id \= 2, Name \= "Displays" }  
    );

    modelBuilder.Entity\<Product\>().HasData(  
        new Product { Id \= 1, Name \= "Wireless Mouse", Price \= 250000, Stock \= 10, CategoryId \= 1 },  
        new Product { Id \= 2, Name \= "Mechanical Keyboard", Price \= 1350000, Stock \= 4, CategoryId \= 1 },  
        new Product { Id \= 3, Name \= "24-Inch Monitor", Price \= 3200000, Stock \= 3, CategoryId \= 2 }  
    );  
}

Sau khi thêm seed data, tạo migration mới nếu cần:

dotnet ef migrations add SeedInitialData  
dotnet ef database update

# **18\) Tracking vs No-Tracking trong Lab04**

| Tình huống | Nên dùng | Ví dụ |
| :---- | :---- | :---- |
| Trang danh sách sản phẩm chỉ đọc | AsNoTracking() | /Products |
| Trang category report chỉ đọc | AsNoTracking() | /Categories |
| Mở sản phẩm để sửa | Tracking mặc định | /Products/Edit/1 |
| Tạo order và trừ stock | Tracking \+ SaveChanges | /Orders/Create |

// Chỉ đọc danh sách: ưu tiên no-tracking  
var products \= await \_context.Products  
    .Include(p \=\> p.Category)  
    .AsNoTracking()  
    .ToListAsync();

// Cần sửa stock: dùng tracking  
var product \= await \_context.Products.FirstOrDefaultAsync(p \=\> p.Id \== productId);  
product.Stock \-= quantity;  
await \_context.SaveChangesAsync();

# **19\) Tạo Order bằng Transaction**

public async Task CreateOrderAsync(OrderCreateViewModel model)  
{  
    await using var transaction \= await \_context.Database.BeginTransactionAsync();  
    try  
    {  
        var product \= await \_context.Products.FirstOrDefaultAsync(p \=\> p.Id \== model.ProductId);  
        if (product \== null) throw new Exception("Product not found");  
        if (product.Stock \< model.Quantity) throw new Exception("Not enough stock");

        var order \= new Order  
        {  
            CreatedAt \= DateTime.Now,  
            TotalAmount \= product.Price \* model.Quantity  
        };  
        \_context.Orders.Add(order);  
        await \_context.SaveChangesAsync();

        var item \= new OrderItem  
        {  
            OrderId \= order.Id,  
            ProductId \= product.Id,  
            Quantity \= model.Quantity,  
            UnitPrice \= product.Price  
        };  
        \_context.OrderItems.Add(item);  
        product.Stock \-= model.Quantity;

        await \_context.SaveChangesAsync();  
        await transaction.CommitAsync();  
    }  
    catch  
    {  
        await transaction.RollbackAsync();  
        throw;  
    }  
}

# **20\) Minimal Test Mindset: kiểm tra Service không cần Controller thật**

Mục tiêu không phải viết unit test nâng cao ngay, mà là viết code để sau này test được. Nếu service nhận repository qua constructor, ta có thể thay repository thật bằng fake repository.

public class FakeProductRepository : IProductRepository  
{  
    public Task\<List\<Product\>\> GetAllReadOnlyAsync()  
    {  
        var data \= new List\<Product\>  
        {  
            new Product { Id \= 1, Name \= "Mouse", Price \= 250000, Stock \= 10 },  
            new Product { Id \= 2, Name \= "Broken", Price \= 0, Stock \= 0 }  
        };  
        return Task.FromResult(data);  
    }  
}

// Ý tưởng kiểm tra:  
// Arrange: tạo fake repository  
// Act: gọi ProductService.GetProductListAsync()  
// Assert: kết quả đúng logic mong đợi

# **21\) Kiểm tra kết quả chạy**

| URL | Kết quả mong đợi |
| :---- | :---- |
| /Products | Hiển thị danh sách sản phẩm từ database |
| /Products/Detail/1 | Hiển thị chi tiết sản phẩm và category |
| /Categories | Hiển thị danh mục và số sản phẩm |
| /DataHealth | Hiển thị trạng thái seed, migration, no-tracking, transaction |
| /Orders/Create | Hiển thị form tạo đơn hàng |
| POST /Orders/Create | Tạo order, tạo order item, trừ stock, commit nếu thành công |

# **22\) Câu hỏi kiểm tra hiểu bài**

10. DI Container giúp gì cho Controller và Service?  
11. Khi nào nên dùng Scoped thay vì Singleton?  
12. Vì sao Controller không nên chứa data logic?  
13. Service khác Repository ở điểm nào?  
14. Options Pattern giải quyết vấn đề gì?  
15. DbContext có vai trò gì trong EF Core?  
16. Migration khác Update Database như thế nào?  
17. Khi nào nên dùng AsNoTracking?  
18. Vì sao tạo đơn hàng và trừ tồn kho nên dùng transaction?  
19. Code dễ test thường có đặc điểm gì?

# **23\) Câu hỏi Problem Solving**

20. Nếu ProductService tự new ProductRepository bên trong method, sau này muốn test logic lọc sản phẩm sẽ gặp khó khăn gì?  
21. Nếu một nghiệp vụ tạo Order thành công nhưng trừ Stock thất bại, dữ liệu sẽ sai như thế nào?  
22. Nếu trang danh sách 5.000 sản phẩm chỉ để xem mà không dùng AsNoTracking, có thể phát sinh vấn đề gì?  
23. Nếu team sửa entity Product thêm cột Stock nhưng quên tạo migration, lỗi gì có thể xảy ra khi chạy app?  
24. Nếu đăng ký DbContext là Singleton thì rủi ro gì có thể xảy ra trong web app nhiều request?  
25. Nếu config SupportEmail được đọc bằng string key ở 10 nơi khác nhau, khi đổi tên key sẽ có rủi ro gì?  
26. Hãy đề xuất cách chia Controller, Service, Repository cho chức năng tìm sản phẩm theo category và khoảng giá.  
27. Hãy mô tả flow rollback khi tạo order bị lỗi tồn kho.

# **24\) Rubric đánh giá**

| Tiêu chí | Điểm | Mô tả đạt yêu cầu |
| :---- | :---- | :---- |
| Cấu trúc project | 15 | Có Data, Models, Repositories, Services, Options, ViewModels rõ ràng |
| DI \+ Service/Repository | 20 | Controller gọi Service; Service gọi Repository; dependency inject đúng |
| EF Core \+ Migration | 20 | DbContext, entity mapping, relationship, migration, update DB hoạt động |
| Tracking/No-Tracking | 10 | Biết dùng AsNoTracking cho màn hình chỉ đọc |
| Transaction | 15 | Tạo order \+ trừ stock an toàn, có rollback khi lỗi |
| Giao diện và luồng chạy | 10 | Các URL chính chạy được, giao diện rõ ràng |
| Git \+ báo cáo | 10 | Commit rõ ràng, có ảnh chụp màn hình và mô tả kết quả |

# **25\) Gợi ý commit Git**

git status  
git add .  
git commit \-m "Lab04: upgrade data layer with DI repository EF Core and transaction"  
git push origin lab04-data-layer

**End of Lab04**

[image1]: <data:image/png;base64,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>

[image2]: <data:image/png;base64,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>

[image3]: <data:image/png;base64,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>

[image4]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAl0AAAFrCAIAAABkK6tRAAA0EklEQVR4Xu3ch5sU1brv8fMvcM/d+3r22Vv3NgIKgiA5SVRBQBADIIKSk+QkQZIkyUmiBEVFJUiQoCJIEiVIHGTIecgwwzTJvu/066wqavX0DIMz013zfZ/P00/1qlXV1dWr16+qGf2v5Bu3AQCA+i+7CQCAXItcBADAQS4CAOAgFwEAcJCLAAA4yEUAABzkIgAADnIRAAAHuQgAgINcBADAQS4CAOAgFwEAcPw1uXj01EUAAHKcnVD36n5zUY8jSFEURVFRUPcfjfeViyQiRVEUFW11nzeOmc9FQpGiKIqKziIXKYqiKOquynQ0ZjIXCUWKoigqmotcpCiKoqi7KnPRSC5SFEVR/ixy8Z7rVMJlAEAU8s7XmSpy8d4qKfkmACBq3X86RmkuJjVtanjXuSrph6qGd11q1d/cw827+l7K/gAAANHmPqMxGnPRHYoRotEdimlFoycU7yca5UTbZ1/0/WCs3Xiv3mnbUx5X/7hJHrv1HWF3iED7Z+4wZNva9Vspe20Eeqiy1csNWrfvNjD+yAm7T1pmzP3aLLfvPsju4DZ+yifupyPGzbD7AICHdwa/l4q6XLRDMWw02qFoR6OdiPcTjfZ5V55A6tl/pDyOmTzb7pmWy1eTft2xVxZeb9IhKYtzUQ/P49TZC1NnzbfbI9BDfa3Ju/p0+NjppxMu2t3CvlwmctGcT3IRQEbczy0juZjRss+7knumk2fODx099WpicpKVizKPDxk15cTpc2827yJPGzbvcvb85TXrf5HlbTvj1m/eLgujJ83SzrqgOfdq4/YXLl1r3Kr76Ekpu6rbsI2k17yvlmnPi5cTX387JZlMf/dhLFiy+ru1myVrZdtrSYGPPv4i4cLlWfMWJbmCyn2D2K7bQF3Y9/vhrxavmv3ZooNHTuoxyOMb73T0LCelHqrJxcTrN8ZNmavLcmw/bdr2/bqfk+7ORXPMdi7KwZw9d6l96mG4O9u5OGn6POn8dpuUO2wACItczMlc7DNojDweP5UgOZcULhd1QdNr1MRZkijS2b0HTZdftu+RG0ftKQm3duNWWT509JRkxpHjp3/ckBKlauSEmRKTJth0z+7DMKt0QQLm2+9+0pawN3CaeUoOePKMz8xTd3yaZXOo7lzUADPH9m6PlMAzL+c+Zk8uHjl+ZvnqlMOTCF+74VdPZzsXdeFef/gFkKv4KheDaUSjt4+ViJ5Q1LITMXOhGEw7F/UHTJOL3fulpNT4qX/+q5jM43LbJwv13+lkNlm0/Icr11JuLpX+W53+E2NSas5Nn/OVPK74fsPLDVpL6shNkq5NuRvbvE0WuvQe5u7vPoyGoXtTIdvqgqjTsE1S6uG5SRRJzpmnLTr00d9IxdAxU3/bc+DTL5d4ls2hmlwcNmaa3MOZYxNtu/RPSn05zzF7clHe3bJV60yLp7Pmovt86gK5CCAC/+eit0eoojMX+w+bIFP2uQtX6jftNGveQpnHp8/9ql6jdrvjDiaFIlCyZNCISUmpv6PO/GSBez9JqTn31eJVdd9sO3XWfL0z+27tZtmJ7DApFBgNmnbW31dNf/dhSNJIvDVp3UO2leVufYc3atE1/nDKn8bo4SW5ckVeRRfEK43+XH6rZbdde+O/+XaNLE+c9unGLTvMstzLmkOtHfq7m3auv7vRYxsyaormonk59zHbv6Ou27j11cbvmptLd2fNRff5NC9tdgIAHn7LRa3IoagVIRHddZ+JqGWf94zg70QAIPv5MxejrdL67zQAANHGO4PfS5GL91D2qQcARJv7uVkMkov3WvYHAACIHvcZikFyMRPl/n/UAgCih3e+zlSRixRFURTlFLlIURRFUU6RixRFURT1Z2UuFJMznYvJRCNFURQVxUUuUhRFUdSflelQTL6fXEwmGimKoqjoK8mmHMvF5FA0ko4URVFUlNT9JKK631xUmo4AAOQsO6Hu1V+TiwAA+AO5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHJnMxTwPPAEAQJSz8ytd5CIAwLfs/EoXuQgA8C07v9JFLgIAfMvOr3SRiwAA37LzK13kIgDAt+z8She5CADwLTu/0kUuAgB8y86vdJGLAADfsvMrXeQiAMC37PxKF7kIAPAtO7/SlX25GAxV4MYNe1UEL9V5UzfUp7pcrMwLdk+zNuPtbp269QvbRw5YN/es1ZY27/bw9Nf2hHPn9en168lm2xZtuspCleqv6qoPx0yWp4/mL/Hf/8h3584d7Sb147qNnn0CADLHzq90ZVMuPvZkSZnx4w8e0YTIOJOLH02bkyfbc3HkmI9MY9CVWOcvXFy+4vtg2rmoW/3tn/ndT3XtufMX3D3dCxnRukPvY2cuxx063bZzX3ut7dMvl9mNUatclTpbd8Vv2bG/fpN29tr7t//wabsxe1Sp8cbO/Uc3/rrnkfwl7LXqy8Wr7cboJwNSbN6279U3W9lr0yVD+pmS1XRZ9mN38OjeZ5inZfueQ3a3v8r0OV8dPH5u/uJVf/vnk/baTJMvpux2yMgp9ioVo4MhCtn5la5sysXtv+2Wqf///m9KTgwdMV4b/0yM1FTQZFr0zbfaMvGjmXlSc7FrzwHaR1dpLv64doM+lZo05eP1G7eYp/O//sb96tpot2i17dgrrVe/det20BVpuvxu5z66EEw7F2/cvLl4yYpft+64dPmK2VCcOHlKlzUyP57zuW6ybftOz37SYiaRBUt/sNfaMpKLe+NP2I3Zr0LVV3oPGKnLEpB2hyg5zkx4rlq9Xu9/qMs/b4/773/ks/vkidmp0ISZfIJDR4WZ6CN/cJnLxT6DRpuWrMvFI6cumuVqNRvYHcJyH1tYZrf/+E+h79b9anfIE7ODIQrZ+ZWubMpFmfovX7miCyYkZKFth55//9efV2GaTKUq1JDlhHPntZvm4tPPVpTHhYuX6+aSi//vX0/JQrtO70mfCZNnamf3zj2vHrbdvSrsq7s31GWZ0eTxqWcqaEtaufhc1Tq6UKBIBV3QtcXLvijLzVp3luTWRr1WkBD17CctnlzcsfdwpRdfk4XnazWUKenpZyvt2n9Mnh49fek/eYtLKGouTp6ZEsApWy1L2arUczVnzVv0z0cKj52cchdupi35Kj7wUMEpH8/3vGj22HfwpN0o50cu1eUxj+s45bavQNGKPfoNL12xVp7UNzv782/0zcqk4z4Vg4ZPnDF3gV7s6/2iZ/NseNfut1aoeJXBIyblcX12evxFS7/w7Q+b5GmJcjXkYP79xLNyP5HHdfxLV60v+Gwl+ewefbKk/RI5yB1mZjnsB+dpV3Yues6AkvOjEWLnop7JTj0GSTCbxr/EJ/OXup+azyLyEZpjC9vt//xPXvfVqr5ls+cq1d/oPXBU2MGgw9UMG2SQnV/pyo5crF2vsWaDqQceLCDtcqOmCST1cN5imkwyaGTVuvWbg3fnYoUqL5vNJReLlqpqnmrlSTv/7HZns9RVYV999544XdBN7ty5U6Rklbu2DAbfadnJ3nPYBdNBb0NNo7a4dxKB/o4qM/u73QbkCU2p2m6uQL9YtErehfmtKWwuHj7552+5ykxb67fsevCxou5V2cm+V+jy3gdxh05L++tvtc2Tepx//9dTA4ZN0A479x+VN9ut91B9Km/2gYcK9B00Rp/KqcgTmnH+X2i85QklomfzPNnyrt1vTY5w+tyv86R+du4P66efU342OHTiz3+c1rtnc/xrN+3Q8xBt7FwM+8HZ7crORc8ZkLf/48btx0K/1uYJl4vO4F+40jTev389WuTDcTPcLeaziHyE5tg83ZQMNvduTS7qnuOPJWi7PRh0uJqvPDLIzq90ZUcuJiYmeYJh+YrvZWHoiPGPFyhVvnJK4DVp3kGTacOmLY/mL2Fiw+SiLN++/Wec6O+ospCQcP6hx4uWKFf9m2UpM6D+9crf/ulcippX1L2pJwqWlqd9+g/T21BdFfbV8xcqJwt9BwyvV7+ZLNR+tYlnt2ndL0ZoWbFqjbZ06NJHWz6aNicY+glXLhcqPv/K3E+/9OzTzT2J5HH9guSeGjxRIY/meygZkCftXBQvvtzIzqfsMeuzxZIZ7pb3+qfMJnIT0PCdDnlSj1P6yK2e6WPnopmVdJaUGcd0llz0bK6y+l3Lvax5a/IJ1qz3dp7Uz859/DoVun+7y3P38f/Pv5+e+clC3Tx6uE/dgaMp03rYD85uV3Yues7Azrgj7rV2LppvwbLVG9wb3r89B467n5rPIvIRmmPzdDPc3ziTi/rUk4v2HrLuR2O/svMrXdmRizLpx+0/4H6qOSHJdOLkqaSk6xMmp/xjnibT3//15PETp/b/Hq+d3blofpPUXPzvf+T7fs1Pcqcld3UNGreWloJFn7tw8ZJ0aNGmq+cA3JUn9NNr4MaNfgNHmJawr54n5XeM6qdOn5Hd1nnNOxkFM5WLEn52n6Klqv22a8/Nm7fksdpLr3v26ZZWLr5Y+80PRn5UoGhF/SbrT3Nzv1iiuZj36bLDRk8tVvbFFT9szuP6HXXMpNl5Qt9MOZn5C5V/odab0piDF6QyC8i7y1eonE6vP++Ik4vrGXMX6DSqxykLcs9RpGS1wiWq6g+SKW/2iWLyjvTNyk7cp8KTi57Ns+1dy1HJp1CuSp1tuw9qi/ns9MOSQzI/nU2b/eUDDxVs17lfHtfxy6cpb7N5u55VX6pv7z8H6cxeqHgVObEa/2l9cJ52Zeei5wx8/OnCEuVfatn+PXcu1q3fwpyHrMvF1h37DB015e//eqpr7yF132huPovIR1g39dg83YxOPQb1HTRGbhCXrPqpSOi9mz3LhnL1EHYw6GgnF++VnV/pyo5czKCwfxEKZNA//lOo3+CxdjuA3MzOr3SRi4h5bzXr9PuRM58vyOjfLgHIPez8SlcU5SIAAH8tO7/SRS4CAHzLzq90kYsAAN+y8ytd5CIAwLfs/EoXuQgA8C07v9JFLgIAfMvOr3SRiwAA37LzK13kIgDAt+z8She5CADwLTu/0pXJXAQAwJfIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5iKjwfx8soOxVHv94tIj2XLR0tb0WAO5Tdueimf5spk/9t9vZa1X3PkPsfXpcuXa9VKXang3/9lDBJSt+sDtnM/sdqZqvvm13zgizB3tV5pgdnjxzPq1V9lb3z9653aLIRQBZym+5+PnXS+2tDLt/NrMPyW3Ltl32JpH95W/N7JBcBJA75Vgu2tOu4c5Fe20EBw4dMxuKQcPHHz+VkHD+8uJl3xUrX+Ne95YV3G8/Kfnm7rj4Jq26uI9ZbnbtrSLI3ImKwH2Eaa2yt7p/9s7tFkUuAshSvsrFv//76QxuuGNXnOmp/ueRZ04nXHD3MasSr9+0d/tG47aePSxcssp+IQ/T2f32ryYmm/a8z1TQxtqvv+PZv3j86bJmq76DRtkdxMy5X2Zk87SYzvYHZFa5G/fuP/S3hwq6X6Vo2RfdHTJ4JO6dR35r7lx84eU3zVr5BD37BIBM8FUumq2mzvrcXmvIfGp6euyPP2LvzU1XPfCfQvYq8eG4afbLuZmenrffvc8Qz0uEjRN3h8jhke7maTHd7A/I3sO3q9fZ+/f0yeCRuBsjvzWTi7YiZV5w7xMAMiHHctHjqWcrmT4R/n3R3qEh83hGuiW7jiF/keeOnzwrWfjAw4Xtbd2vu2DJyktXkiZMnSPt7w0YoY0PPlHM7my/nJvp5kkd++A79Rx4PXAr7LZHT5yxG909M765zXSLwO68Oy5eW3r2G6YtdRu00JYMHkmEnbu3Tb47F+Xlrly7/vLrTdPqDAD3yj+5uGvvgYx0+3XHnrDd7EbT4gkS07595z7TWOK5mto457OF7s4eZltPLob9qXZP3MFGzTo8Uaic54fK6bO/sHdov1ZGNre5e6ZFe279ba+nxbMH05KRI7G3sluUycVnSjt3h2l1BoB7lWO5aP9MZ2Tud9SkZCdaDh87ZXdQU2bOC7tz07h3/yFPi2cPpj2s1h17e/qH3dbz9t0/SGqL3MvaO1fuX2s9WxkZ3NxmutkfkOe13GcyLO2WwSPxbBW2RZlcnD3v63Q7A8C98k8uJrt27vnTD7cvF30bduem8ey5S54Wzx5Me1hyY+TpH3Zbz9v/1xPPul/u94NHzVO5qbp8NeWPVJ8uUUVbIsfJPW1uMxvaH5DntdxnMqzkezkS91Zptaiwf4+aVmcAuFe+ysWmbbqZDdt16etZ+0bjtvJ4PXDL9JFZW1fNX7jcfkW7ReUtXF7b9c9A7onZp/vtV6vVwLRPnDZXWiZN+0Sf/jt/CXvbsHEit8umMeOb20w3+wMyq/Sp+0za+1EZPxJ7V6bF/daSyUUAWSzHctFm+mQ6F5PT/ktR994eylvcXqWq1qxvduXZynD/jczAYePOXbxyJuHigiUrKzxfz+7sYb+imySudnP/I+jqNRvlJUpUeMm0hI0T4542t5lu6eaikLTTFlnYsnVX4vWbe+IO9h8yRlpqvNI4+V6OxN65afGsIhcBZCm/5aIoWbGWvXPP3v73sTB/61+weBX3fuytDPf9pYfdOew+beYPOJV9hGF/fpwwdY6n2z1tbjP9M5KL4t/5/oxGD83F5AwfiVlrWtJ6a+QigCzlw1xMDv3EN27yx6UrvyyT8hOFyrVo38P+S5ztO/fVqd9cbnQeLVCmUbMO9n+9kO4xjPtoltz9PPBw4bzPVHjuxVd79R++6Zcddjc39/v95+NFnyn9gry0pKzdU4wYO/WRAqXlJrL/0LHytFDJqrqhJ9hWrdlQrHwN86ee97q5hzm8DOaiiD98onGLTnKe5R09W676q41azZ739flLV02HjBxJ2J2HfWvkIoAsld25CABANCMXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAACO7MvFB/OVBgDEHHs+9zdyEQAQiT2f+xu5CACIxJ7P/Y1cBABEYs/n/kYuAgAisedzfyMXAQCR2PO5v5GLAIBI7Pnc38hFAEAk9nzub+QiACASez73N3IRABCJPZ/7Ww7n4mdffpOYdD1fkcqNmnXK+0yl9Zt+PXHytN0tLcFg0G7MPeTt16j3jt2eCbKrUpVfsduN56rXj8WzHQzVU8Wq2atGT5iRwXeUwW6xpXXHPucvXHqs0HOdew3WlrTGQMZPlJ98uXC5vOtm7Xo+GDozVWq+afdJ64z5jz2f+1tO5uK/nywrA+uRghVMi+TiyVNn9uz73aRjuy79EhOT9sYdePTplG4t3n3vytVrSdeT6zZo9WDqhDVv/uI7d+7Y+/c9dy6+3ri9no1de/YfPnpc1/YfMjY5OdCxx0Dt88vWnXIVMnD4eH26cfNWPZM7d8dpfuw/cMjuNvXjz+Sc9x44MuYmx4rVG8gxy4j6ddtObdHxs2nL9gdTI1PflD7KyTRPpWTUPZS/jFnrM2fOnrt2LfGJwpX0qXsMLFyyUhauJycPHjHxwYgnqtZrzX78abOMsY9mfGq/REzTXDTvWnNRriHkK3PoyPF8RatE/tb4jD2f+1tO5mKdBi112BmSi7dup/SU9qGjJherUFs7NG/fSxaeLV/L01+efrP8OxmO9s5zA/1aSn3/4wZ5um7DloRzF8wpkoXCpavrgjzevn1HTqk+bdS8s5znxctWu3elV76eboVL19DN23ft7zn50e/UmbP79sfXfLWZHrln/Lhvg3TBTPdKZnyZ+8xan2nSqmvK0AmVfNEedI0BQ994hBOlm5d47mV7/7FOc1EuMfsMGhUM5WLxCi/ru27UrJN5+2G/NfbeYp09n/tbTuZi2PtFvVOUdvk2NmvX03whRbO2PdxPtZvM75IHnj3nEsG7f0etVKOBtGz7bY9Zaxb+81TKqe7WZ6h7236Dx7if6jfc0+21xu10P57MiAlBV9V7s41n/NjTfe03WsiCTn8Fij+/dv3PV69eM2t9qWHTjvLulq9c86BrDMid4tIVP2jLg2mfKFmo/srbK1av1TOcr0hle/+xS3Pxofxl9N1JLprpSM6SLpgzFrz7W+M/9nzubzmZi+Lzr5YkJiblfaZS/bffdf/7YjCUi7rQruv7ZarW27L1N30q4y//s1XnzV+sT+Xxjz/+yJ3RGLw7F+WpnKig6x9F5CzJ6dKzNH7K7Fu3bslJlju/J4tVGzvpY7nIdZ9JuSm3u+kqueXaGxev+4kVnXoOMge8cMnKa9cSH0wdP1VrNZLljj0Gmg6y8FK9pnG/HzTnUM6MPPo4F79bs0E+VgkzeXeVX2r4oGsMyACQL+bI8dP1jad1oh4M3VI3btn18cIVg3df4PqA5qIsyMwTTP0dVRbkmuCXrTt3792vT9P61viMPZ/7Ww7nIrKOL2dzANnPns/9jVz0LXIRwF/Cns/9jVwEAERiz+f+Ri4CACKx53N/IxcBAJHY87m/kYsAgEjs+dzfyEUAQCT2fO5v5CIAIBJ7Pvc3chEAEIk9n/sbuQgAiMSez/2NXAQARGLP5/6WfbkYuAkAiD32fO5v5CIAIBJ7Pvc3chEAEIk9n/sbuQgAiMSez/2NXAQARGLP5/5GLgIAIrHnc38jFwEAkdjzub+RiwCASOz53N+iLhd79R9uN0aWiU1ylbdbdzGPYbXt0sdu9CXPSbBHToSzBGSRzr0G2o1RxZ7P/S2Hc7FkxVrnL12RhSJlXmjwTnu7QwR9Bo20G3ObGq803hMXb7e7pTvX+zUX673Z0m4UbzXvaDcqPVdHT5x+tVGrK9eur16zPilw0+7mEWGHUevQ0ZPyjbuWlNy4RSdZDqSerseeLmN3zoU6dH9fHk+cOlviuZdkJARSz0/bzn3kpNn905KRacrOxYxslZ3s+dzfcjgX5ZtZ89UmsjDuo481F/USvmzVOmcSLjyUr7gsv964zax5X5lNuvX5oFL11wKuoaObyPBNOH9Jv9X/ebLkhUtXnyxa0f1a/vPFgqXyfS1cqpo+ldOVcOFyoZJVPcvu+8WR46dt2bZz3cZfvv5mReDu8+w/OpG5B4OeBBNj5n7xxOkEHVTa4eGnSrn3Y0ag9Ll0NdGc8EBoNCYl3zA79JzeaPbwkyU9y3K6CEVDc1G9VC9ljtLh1L5rv8TrAbNq4rQ58tiqQ69AaJxMmDJbPv3JMz4xHcw05Z6gVN/Bo7bt3CsdTC7qcHJvZQ+5HGHP5/6W87nYsUf/UROm6XIgNFXt2nfgwKFjstykZadAaLRpZ2mX8SGTuM7j7lzcHRcfd+CwLPfoO0QeBw4fp+3u1/Ifnc4Gj5igT3v0GyqP8rWUW3D3sjsX5dTJVv/OX6JMlZfNeZY7BnvnPqATmXswhM1FGWZyQnRQmbPk3o+OQJnC5NRJyupaMxq//W6t2aH79Lr3EIXc71GXK7/0RtVaDeyeuZM7F58p/XwgdTh9OG7q7wePmlWeXNRG97nVacozQXm6SS66h5PZyjPkcpA9n/tbzudiIPQjqlmWqUqujxYtXRUIXWEFrNG29bc9utB7wAhtl02uXLs+f+EyWX7h5TflcdCI8dquHXxJrisXLFmpy3KbEgj9phpI/aK6l925+EShcmYPl68lLQyd52Lla5hGP9GJzD0Y9CS806ardpDGI8dP7Y8/EkgdXaaD/nSmdATKsJw0fa5pNKNx6YofzA7dpzfKyTu9mpgcCA2kJq06B0Kn63TCBb1vhsnF+MPH5S4w4PpZXkeUGjJyojw+X7thwDVTPVuuuumg05RnglJ67yjnX3LRPZzMVp4hl4Ps+dzfoiIX3cs6f8mgLFr2RW0xo+2HdZseLVjmoxmf5n2mvDx9+Y1mOph0k/5DxsjaH9f/HMgduag300rPQ6eeA+WSc8DQsZ5ldy4GQr/MlKpU++SZc9pNzrNZ5TNp5aLMdHkLl9+7/6A2ygkpWbGWDipzKuSCXYaT3giaEThz7nxpfG9AylZmNMpEZnaoezOnN8p16/PBw0+V6vLeIH2qpyvhwuVy1V6xO+c2MgXJ16pQyaoz5nyhLQ+FfqnSf/cx2nXpK5eV5petFu/2LFC8svvfpM005Z6glFyY5itSQc6/5KJ7OLm3cg+5HGTP5/6Ww7mYFhkNJ06djaGr72jgvox1LwPIBub6yX/s+dzfojQXAQBRwp7P/Y1cBABEYs/n/kYuAgAisedzfyMXAQCR2PO5v5GLAIBI7Pnc38hFAEAk9nzub+QiACASez73N3IRABCJPZ/7G7kIAIjEns/9jVwEAERiz+f+ln25eOzM5SBFURQVUyVTtz2f+xu5SFEURaVZ5GIWIhcpiqJirsjFLEQuUhRFxVyRi1mIXKQoioq5IhezELlIURQVc0UuZiFykaIoKuaKXMxC5CJFUVTMFbmYhcLm4oChY54oXG7spBneFeHq3a593U8fylf8kadKvd649a3bt93tWv0/GOVtysX1/uCR+Z6psHDJCu8KX9fpMwmFSlaV9+5dkYHyDDa7fDDAevYb+kiB0j36fqBPX23UUh7Xb9rSf8jou/rlyurUc4DMMKUr105Muu5dF6669R7sbfJLkYtZyM7FGzdufrVomXn6xx9/lKtap1L11/TptI/nPfZ0mc++XCzL3fsMLla+etM2XU3nYOrXWEq6BUNfchnHjVt0DKaOaeFu9F+ZGS2DVeK5mma5fpO2bTq999pbrW7evFm07Isv1WssjfmLVAiGLjjkcfjoydqzet1G5avVvX37jtk2Vkrel6cl5V137PV0iSp37qS8HZnL8hd9ThYGDx/3aMEyGzb/Erx7sD1fu6FuqLPe9FnzHi9UdsvWHWaArf1pU97C5SdPn2NeIlaqWdtu8gWUBbmsbNq6SzD0hZo3f9GnXyzwds2VJR+xLpSsWCt493TknlUCgRsyhOSbKCPki6+/uXXrljTKGHN2FPtFLmYhOxel5CZGvpOHDh/VZW1s8HZbGW0/bdwiy2MmTk9ODiz59jtZLl6hhmtTJxeHjpxgGuWrvi/uQPDuy3nT6L+aOGWWTOg//rTJu+Luivs9Xr7J7vtFSQhd0KuKYOiM1Xy1iSyMnjDtbMI5uZOQ5aPHTpw8dTp1oxirEWMmS4wNHDbGtJh3rWOpYdP2wdDU9vXi5bLwwstvegabOxevJye7M0MHmF5DxGK5j1yXZSAtWb7a6ZG7S3Pxl62/yQftno5MB51VZIAFQ9cWeuXUpGUneXy2XHXTzQdFLmahsLmoJUNQbkfcX9Sdu/f9Hn9Il/cfOHjs+ElZeLtVZ9Mh6MpFufINhi7rTpw8LfcBK7/7MZg6bXkafVm/btspV6x6uvQmRm36eaunp/uG2ySEOe0SEmvWbZT9yGch179ydaLtm7dsq1a7gfk4YqjeaZ0yYORK37R43rVeUckA0yszuQ/wDDZ3LsqYjD94RJ8GUwfYH3/8Iekr95emPVbq4adKepblC1W1Zv3zFy46nXJxaS4uXrpy2crv3dNR8O5ZxXx9NBdlwOzZt1+Hk2+KXMxCdi7KBde3q9bIFbr+diq3KTIKZcCNmzxTnj75bMVr1xLnfva1LJerWufS5Suea3P5GsvQlE3q1G8qTwsWr5x0/bpc8msE1mvYwm70Wb3VvEPlGq9fuHDJu8KqLVt3yHz3nyedqdAkxAcjxu/YuWfspBmaBzpFyk2kXCnLwrYdu/bG/S5fdf0gYqvkWl5u8jy/Hk+ePkeSfsrMT4KuXxpKVap15epVvXV2D7aR46ZIFk6Y8rHOeoVLV0tMTOr1/rBg6gDr2W+I3EzYP9hGf508dbpRs3dv3bolVw/6k4BeaFav2+hswjlv79xX5ndU+dCDd09H7llFLo92790vjzpCZBTJbbdrN34ocjEL2blIUdlc5mqAorKo/PcHOORiFiIXqRwvcpHK0pLbSm9T7Be5mIXIRYqiqJgrcjELkYsURVExV+RiFiIXKYqiYq7IxSxELlIURcVckYtZiFykKIqKuSIXsxC5SFEUFXNFLmYhcpGiKCrmilzMQuQiRVFUzBW5mIXIRYqiqJgrcjELBW4CAGKPPZ/7G7kIAIjEns/9jVwEAERiz+f+Ri4CACKx53N/IxcBAJHY87m/kYsAgEjs+dzfyEUAQCT2fO5v5CIAIBJ7Pve3HM7Fh/IV9yy4Xbx8TdoNu4PH643bmOW3W3exO/hMvTdbmuVe/Ye7V73VvKPdPyzPhj4Tdth06P7+iLFTZGHDz9tOnDrr6W907zPE3tZt0IjxZjkWx5u8wUcKlO7ae7A+1eG05qfNfQePsjvnNjJIZAyUqlRbZiF7ra1zr4F2o63PoJG6kNb3LoMDqW2XPnZj1rHnc3/L4Vxs8Ha7Hv2GBlLnrwFDxz5asMyP639295k++wtdkJGXv+hz8mWWzgVLVNkffzj0gd0qV+2VGq80DqTm4rffrf1iwVIdXtLSrkvfQiWrJgVuXrqamK9IBZkFMjiCo5+diy/WaVSuWl15szqz791/UNdeS0ouUvbF6q+8Fbj7nOiGn85flHg9IMst3+1pv0rsunwtaeX3P/UfMkafmpMjU97ztRsGUnPRfXLUY0+XCaSOt0BocIYdb5qL2kfHW5f3BhUp80LvASNkOcrH2zttul5NTJaFpOQbTVp1DoSG05zPFsya95XdOReSQaILJSvWlMfJMz6RUTH384WB0PWEDIlGzTvI8pVr12UAeD5lz9dNvlYyfq6HBp65xJfvnaxq1rZbgWKVDh87lfeZ8jrR6UAyF2eelw6Extiz5apnMD7/KvZ87m85nYvvtH/trdaB0DiQESZ5Jss6ZxkmFyVE3e0PP1lSHp8oVM60yDibMvPTnzb9GkgdXuYOsmOP/pK4siDTYnTOU5ngycUDh44dOnpSn3ruF3WiD4QuV93nRDc0/YuWfdG9Vax7pWELeZSgkkf3ydEpr3m77pqL7pOjC9qS7niTXJRJTZd1vDVt000e23ZOuZaP8vGmc657WQ54wZKVds/cSQfJxi3bZFTI1PTDuk3ydOT4aaaDXFX8tjvu8UJlA6FrC/enHPbrVqx8DW3Rp5qLuqwDpmTFWgHX/eL5S1fk2sX90m80aSuXeguXrjJ7yzb2fO5vOZ+LgdD8Jd/MXfsOxB1IuSTX36/M5ZLJRb08l8u3Hn2HyP3NIwVKazezNxlnRVJndk8uylPTMzrnqUyw7xfXbfylaq0Gu+PiPblo3rvM9e5zYjasVrvB9l379Pz7hrnoluvxgOvk6JTXqkMvzUX3ydEFndd0vMkNYlrjTaatPgM/1GU9mbqJntIoH28a8+5lGU6VX3rj5JlzdudcSAfJV4u/Xbxs9dYde2TYmFUyBR06ekLu/5au+CHspxz266aNYXNRf9WX72DAlYtPl6wqj56Xlkky/vBxWWjcopNpzAb2fO5vUZGLMkR00MgVU8KFy+ZqS3lysUDxyh98OGHnnv26yZCRE5evXvvr9t2B1BTU+wM7F+VVZOofMHRsdM5TmeDJxc2//rZjV5y8x48/+VK+fu5/Fxk4bOyWbTtHTZgmX6qwuSinXe9vfGPZyjVyxa3LMq7cJ8f8RCYX+5KL7pOj7e5clERMa7xJhz1x8TrT2bkY5eNN7p4bNn1X3l2Tlp30TlqH04t1Gh2/+99ccyczSAqXqiaPTxatKMNJBk8gNAVdupL4fO2Gkovy6W/buVce3Z+y5+s2YcpsuSabOG2OrKrboLn2iZyLeQuXN3szLz1qwnR5WrZqnbPnLrqvz7KBPZ/7Ww7nYvaTy38doPCIzuk71jHecjkTfjHNns/9LRfl4uVrScXK13DfY8GQa1K7EfeD8YYAuRibclEuAgAywZ7P/Y1cBABEYs/n/kYuAgAisedzfyMXAQCR2PO5v5GLAIBI7Pnc38hFAEAk9nzub+QiACASez73N3IRABCJPZ/7G7kIAIjEns/9Lfty8diZy0GKoigqpkqmbns+9zdykaIoikqzyMUsRC5SFEXFXJGLWYhcpCiKirkiF7MQuUhRFBVzRS5mIXKRoigq5opczELkIkVRVMwVuZiFyEWKoqiYK3IxC9m5+FC+4kbPfkM9azNX/T8YpQt9Bo64e40P68zZc8Ur1BCy4F0Xqm69B3ubclNduHipXNU6cn42/bzVuy7DpQPJjKte7w8LhobuI0+VGjB0jLunVNM2Xc1j/SZtPWvD1tCRE7xN2VXypXukQOkefT/Qp682aimP6zdt6T9k9F39cmVdvXqtdOXaVV56QxY8qwoWr+xpyUh5BlIMFbmYhexc1Hrs6TLB0Ayev+hzwdCM83SJKoeOHAuGZpYO3foVLlXt9u078rR63Ublq9XVZXe3P/74o8Lzr9Ss16RTzwGassHUUTh4+LhHC5bZsPkXWe7eZ3CRMi/0G/SheelYr5IVa+mCzP7B0Olq0+m9195qFQjckJMp852cVTk5srZS9de0pznPWmFP6adfLLh1+7a0tOvSp2nrLp49xFCZ86PlHgwdu7//TOnnBw0fKxlWrHx17RC2UQaSe1zp2dMI0fy4efNm0bIvvlSvcdDKxTYde8kpvXMnzREbTM1F2e3Xi5frK2ZPNWvb7caNm7Ign7V8ysHQm5o3f5F8+t6uubLkusf91Hx8Ld/tIcvyjbC/F2vWbZSTKX304qnB2ykXRuYb5x5I9rbRXORiFoqciw2btnc3PvxUyaDrirtzrwFHj504eeq0u08wtdsThcuZFvf9osSDzjUvvPymPDZv110e23ftazrHdO2N+33ldz/qsnwhd+2JM6fr8UJlg6H5Tr6T+Z6poI36LXWf5winVC5H5PHJZyvKOfTsIVZqX9yBFavXmKeewdCifcpgkLsBXfv5V4vTavRc5s9fsCQYihA5vYVKVg2mDmDtE/Z+UW5YdSEYbsRKLhYolpn7j/ssjXn3slw0LFm+2umRu2vRkhXy+U6ePsfdqB9fjVfeksew34uJU2bJDCNfwOvJyb9s/S3o+sa5B1LYbaO2yMUsFDkX9apZLqPkUuvWrVuPFCgddM0sOtFs3rKtWu0Gv8cf8nRzf8Pdubj/wMFDh48GQ78XBVNfwje/ryYmJk2aOluXZ8z+7OrVa+Z0mRMiueg+OUHrV7u0TulTxSqdv3Bx4+Zfg3ef3hiqlPMzzZnUwg6Gug2a6dopMz9Jq9E9nX21cKmuklyUmy0duub8yAwYNhf1/iCtESv3be8PHmmeZlvpFO9eljdVtWZ9+dydTrm+5F4/b+Hyno9PczHs90ICT64mR42fOnz0ZG0x3zj3QAq7bdQWuZiFMpKLMvKGjZ60b3+8jht3Lm7bsUvukPbs2z/3s6893WQIrv7hp9927ZXleg1b6CY6CktVqnXl6lX3S/gmF6WeLVddpvv4g0dkIeg6XfLd2713vzxKLo6eMG3x0pUnTp4eN3lm8O5cjHBKN2z+pXKN17WbZw8xVCUqvLT9t90J587rmbEHQ8ZzUceVTHm6Sn9Hlej9cOxHH4wYv2PnnrGTZhw7ftKTi3K3IVcesp8II1Ze9MDBw9n/z04nT51u1OxdObB3WnfWnw30TVWv2+hswjlv79xX9d5seerMWfmOyC2d5+PTXAz7vaj5ahP5uAOBG+ayw5OLOpDCbhu1RS5mobRykaJipXr2G+Jtoii/F7mYhchFiqKomCtyMQuRixRFUTFX5GIWIhcpiqJirsjFLEQuUhRFxVyRi1mIXKQoioq5IhezELlIURQVc0UuZiFykaIoKuaKXMxaRCNFUVQMVS4MxWRykaIoikqryMXsIGeZdKQoiory0rnansNzg+zORaVnHAAQnex5O/fImVwEACA6kYsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHORiTBox+Qu7EQBw/7I7F4dN/OzKteSd+w5/uuB7d/vGrXvPXbzWvs94Wd66K37t5p3Lfvj590MnPasuXE4cO2PByTMXV63bau/cmP3lqmZdP9TlMdO/lv0cOZGwfM0WeTp04meJ12+26z1O17bqMTrhwlVZaNplhDxOm7fMs6uWPUa5n54+d3nYpM+vJgXkIJOSb3k6p0V227rn6EtXr8tyx/cnjpwy/+cd++Vd6Npug6bIY9dBH+2LP372/JXhkz43G8rR2ntLtnKxebeRcmbkUZ/KriZ8vMjeCgCQrmzNRZnlZ3z+7fHT52VZ0tHu0KxrSji17jVGn/b4YKpnVc8h0+ytbF0HfrRp6z7JP1mWCNQFpUkz9+vv9KlElMZJBnPRZI+SF5JA0j6SVd/9tE0Weg+feSUx0KHfBNNNdvv9+u2Dxs6V5W9WbZIXTdl20EfyuHlb3KmES5evJX+zepN7z8qTixLhsvPk0GstWb15V9wRuXq4Hri94sdfpPGLb36Ux6mfprwFchEAMidbc3H8xwv1tk+540rMX7o27mDKDaJGVLIrIM2q5t0+3PDrXslUCST3tm7HTp2XpJGFUVO/1JaftuyWQNVbTNm5ZNu8hT/oKomo/YdO7os/kcFcNMcmDh8/e/TkOVmYNX9lcuo93IHDp+Qg5W7V3VN3O23e8oUrNiSHXlQeh06Ylxx6R/IYf+T0z9v3m/6GOxflxlcuDnS35n5Rnspt6Ppf9sjysu9/PnHmghxAMrkIAJmVrbn446adF68kdeo/SWbz6Z8td69a8O363fuP6rKZ9Kd8stSzatLsxbogGeDe3E32rwsaPIbecXruwDSiWnQfmcFcHDdzwdWkgC7Lwrqfd8nC+6NmJ6cettwp2sdmdtvx/YnJqS96NTEg93nzFv2Z0Hr76GGO9nTCZcn75NRgNqeo84DJyaGjkseBY+aYDclFAMicbM1FsWnrPrkLHDL+rsRKDk33Sp/K7V2f0A+G9qp+H34sWTV/yVrPHgxzm3jyzMXtuw9KpnYZOLnHB1MvXE5MdiWN/hOjRlTCxasmF/W1zK2qJxfFJwu+a9F91OhpX8mypJos/7bvcLIrq+TGVBrnfLXabOKJW33R5LvvPuUY5CAl53aG9mY6mPfee/jMboOm6IHJa02ctVhuvvXfONdu3imvuGWHc8dJLgJA5mR3LgIAEM3IRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBwkIsAADjIRQAAHOQiAAAOchEAAAe5CACAg1wEAMBBLgIA4CAXAQBw/H/vw9xfzHs58gAAAABJRU5ErkJggg==>