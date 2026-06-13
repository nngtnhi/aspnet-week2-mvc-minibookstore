

**Lab04: DI, Service/Repository, Options Pattern & EF Core Data Layer \- Requirements**

## **Câu 1**

Hãy thực hiện lại đầy đủ toàn bộ nội dung trong bài hướng dẫn Lab04 trên chính ứng dụng đã làm ở Lab03, hoặc một ứng dụng khác có mức độ tương đương.  
Ứng dụng cần được nâng cấp từ một website MVC có giao diện và form cơ bản thành một ứng dụng có Data Layer rõ ràng, sử dụng database thật và có luồng xử lý nghiệp vụ an toàn.  
Bài làm phải thể hiện được các nội dung cốt lõi sau:  
· Dependency Injection và lựa chọn lifetime phù hợp cho DbContext, Repository, Service.  
· Luồng Controller → Service → Repository → DbContext → Database.  
· Options Pattern để đọc cấu hình theo dạng strongly-typed thay vì dùng string key rải rác.  
· Entity, DbContext, Entity Mapping và Relationship cơ bản bằng EF Core.  
· Migration, Update Database và Seed Data.  
· Tracking và AsNoTracking cho các tình huống đọc hoặc cập nhật dữ liệu.  
· Transaction cho một nghiệp vụ gồm nhiều bước ghi dữ liệu.  
· Minimal Test Mindset: dependency được tách để có thể thay bằng fake repository khi cần kiểm tra logic.  
· Quản lý source code bằng Git và đưa project lên GitHub.  
Sinh viên có thể tiếp tục một trong các hướng đã chọn ở Lab03 hoặc chọn bài toán tương đương có dữ liệu liên kết và nghiệp vụ nhiều bước, ví dụ:  
· Mini Shop / Equipment Inventory: Product, Category, Order, OrderItem; tạo đơn hàng và trừ tồn kho.  
· Mini Medical Supplies: Supply, SupplyCategory, Issue, IssueItem; xuất vật tư và trừ số lượng tồn.  
· Mini Bookstore: Book, Genre, Sale, SaleItem; bán sách và giảm số bản còn lại.  
· Mini Training Center: Course, Student, Enrollment; đăng ký khóa học và giảm số chỗ còn trống.  
· Mini Library: Book, Author, Borrowing; mượn sách và giảm số bản có thể cho mượn.

## **Yêu cầu**

Vẫn thực hiện đầy đủ các bước như trong bài hướng dẫn Lab04:  
· mở lại project Lab03 hoặc tạo project ASP.NET Core MVC tương đương  
· tạo branch mới để thực hiện Lab04  
· tổ chức các thư mục Models / Data / Repositories / Services / Options / ViewModels  
· tạo các Entity phù hợp với bài toán và khai báo Relationship rõ ràng  
· tạo AppDbContext, DbSet và cấu hình mapping cơ bản  
· cấu hình Connection String trong appsettings.json  
· tạo class AppSettings và bind config bằng Options Pattern  
· đăng ký DbContext, Repository và Service trong DI Container với lifetime phù hợp  
· tạo interface và implementation cho Repository  
· tạo Service xử lý nghiệp vụ; Controller chỉ gọi Service, không tự query database  
· tạo Migration ban đầu và chạy Update Database  
· tạo Seed Data và kiểm tra dữ liệu mẫu trong database  
· tạo trang danh sách lấy dữ liệu từ database  
· tạo trang thể hiện Relationship giữa các Entity  
· dùng AsNoTracking cho ít nhất một truy vấn chỉ đọc  
· dùng Tracking cho ít nhất một nghiệp vụ cần cập nhật dữ liệu  
· tạo một nghiệp vụ có Transaction, Commit và Rollback  
· tạo trang Data Health hoặc trang kiểm tra dữ liệu tương đương  
· thể hiện minimal test mindset bằng cách tách dependency để có thể dùng fake repository  
· chạy chương trình bằng dotnet run, kiểm tra các URL, commit và push lên GitHub

## **Ứng dụng mới phải có**

Ứng dụng sau Lab04 phải có ít nhất:  
· 1 AppDbContext và ít nhất 4 Entity có ý nghĩa nghiệp vụ.  
· Ít nhất 2 Relationship giữa các Entity, trong đó có ít nhất 1 quan hệ One-to-Many.  
· Ít nhất 2 Repository và 2 Service được inject qua constructor.  
· Controller không được tự tạo Repository/Service bằng new và không query DbContext trực tiếp.  
· 1 class cấu hình strongly-typed được đọc qua IOptions\<T\> hoặc cơ chế Options phù hợp.  
· Ít nhất 2 migration có tên rõ nghĩa, ví dụ InitialCreate và SeedInitialData hoặc AddProductCode.  
· Seed Data đủ để kiểm tra danh sách, relationship và transaction.  
· 1 trang danh sách lấy dữ liệu thật từ database.  
· 1 trang báo cáo/chi tiết hiển thị dữ liệu liên quan qua Relationship.  
· 1 trang Data Health hoặc trang kiểm tra migration, seed, tracking/no-tracking và transaction.  
· 1 form thực hiện nghiệp vụ nhiều bước có transaction; nếu lỗi phải rollback toàn bộ.  
· Ảnh chụp hoặc mô tả chứng minh truy vấn chỉ đọc dùng AsNoTracking và truy vấn cập nhật dùng Tracking.

## **Sinh viên phải đổi toàn bộ**

Sinh viên phải đổi toàn bộ các thành phần sau cho phù hợp với bài toán của mình:  
· tên solution/project nếu tạo mới  
· tên Entity, property và Relationship  
· tên DbContext, Repository, Service và ViewModel  
· tên Controller, Action, Route và View  
· nội dung AppSettings và các giá trị cấu hình  
· dữ liệu Seed  
· nội dung giao diện, thông báo và nhãn hiển thị  
· tên Migration và commit message  
**Không được copy nguyên bài mẫu Product, Category, Order, OrderItem, ProductRepository, ProductService hoặc các dữ liệu seed nếu sinh viên chọn một bài toán khác.**

## **Ví dụ chuyển đổi bài toán**

Nếu chọn Mini Training Center, có thể chuyển cấu trúc Lab04 như sau:  
· Category → CourseCategory  
· Product → Course  
· Customer → Student  
· Order → Enrollment  
· OrderItem → EnrollmentDetail hoặc bỏ nếu nghiệp vụ chỉ đăng ký một khóa học  
· Stock → AvailableSeats  
· Create Order Transaction → tạo Enrollment và giảm AvailableSeats; nếu không đủ chỗ thì rollback  
Các route gợi ý:  
/Courses  
/CourseCategories  
/Enrollments/Create  
/DataHealth

## **Câu 2**

Hãy thực hiện các modification hoặc feature nhỏ sau để chứng minh rằng em hiểu cấu trúc và luồng xử lý của Lab04, không chỉ làm lại theo bài mẫu.  
**Mỗi feature phải đi đúng luồng Controller → Service → Repository → DbContext. Không được chỉ xử lý trực tiếp trong View hoặc Controller.**

## **Feature 1 — Cấu hình ngưỡng bằng Options Pattern**

Bổ sung một cấu hình có ý nghĩa nghiệp vụ vào appsettings.json và sử dụng cấu hình đó trong Service.  
· Ví dụ với Mini Shop: LowStockThreshold để xác định sản phẩm cần nhập thêm.  
· Ví dụ với Training Center: LowSeatThreshold để xác định khóa học sắp hết chỗ.  
· Ví dụ với Library: LowAvailableCopyThreshold để xác định sách sắp hết bản cho mượn.  
· Không được hardcode giá trị ngưỡng trong Controller hoặc View.  
· Thay đổi giá trị trong appsettings.json phải làm thay đổi kết quả hiển thị mà không cần sửa code nghiệp vụ.

## **Feature 2 — Trang lọc dữ liệu theo Relationship và khoảng giá trị**

Tạo một trang lọc hoặc báo cáo có query từ database, sử dụng Relationship và AsNoTracking.  
· Ví dụ route: /Products/Filter?categoryId=1\&minPrice=100000\&maxPrice=2000000  
· Có thể thay Product/Category/Price bằng Entity và field tương đương trong bài toán của em.  
· Repository chịu trách nhiệm query dữ liệu.  
· Service chịu trách nhiệm xử lý logic và map sang ViewModel.  
· Controller chỉ nhận input, gọi Service và trả View.  
· Kết quả chỉ đọc phải dùng AsNoTracking.

## **Feature 3 — Thay đổi Entity và tạo Migration mới**

Bổ sung một field mới có ý nghĩa vào Entity chính, sau đó tạo migration và cập nhật database.  
· Ví dụ: SKU / ProductCode / CourseCode / BookCode / SupplyCode.  
· Cấu hình field bằng Data Annotation hoặc Fluent API, ví dụ IsRequired và HasMaxLength.  
· Tạo migration có tên rõ nghĩa, ví dụ AddProductCode hoặc AddCourseCode.  
· Cập nhật Seed Data và hiển thị field mới trên giao diện.  
· Chụp ảnh hoặc mô tả chứng minh database đã có cột mới.

## **Feature khuyến khích — Trang lịch sử nghiệp vụ**

Tạo trang danh sách lịch sử Order / Enrollment / Borrowing / Issue tương ứng với bài toán của em.  
· Hiển thị dữ liệu liên quan bằng Include hoặc projection phù hợp.  
· Trang lịch sử chỉ đọc nên dùng AsNoTracking.  
· Hiển thị ít nhất: mã giao dịch, thời gian, đối tượng liên quan, số lượng và tổng giá trị hoặc thông tin tương đương.

## **Yêu cầu chứng minh cho Câu 2**

· Giải thích ngắn gọn feature được đặt ở layer nào và vì sao.  
· Chụp ảnh giao diện trước/sau hoặc ảnh kết quả chạy.  
· Chụp ảnh migration mới và cấu trúc database sau khi update.  
· Ghi rõ URL dùng để kiểm tra feature.  
· Có commit Git riêng cho các modification/feature.

## **Câu 3**

Hãy trả lời các câu hỏi Problem Solving sau để rèn tư duy phân tích và giải quyết vấn đề trong ứng dụng ASP.NET Core MVC có Data Layer.  
Dựa trên ứng dụng mà em đã xây dựng ở Câu 1 và Câu 2, hãy phân tích và đề xuất hướng giải quyết cho các tình huống sau:  
1\. Nếu đăng ký AppDbContext là Singleton trong một web app có nhiều request đồng thời, rủi ro gì có thể xảy ra? Em sẽ chọn lifetime nào và vì sao?  
2\. Nếu Controller trực tiếp query DbContext và chứa luôn logic nghiệp vụ, việc bảo trì, tái sử dụng và kiểm tra code sẽ gặp khó khăn gì?  
3\. Trong feature ngưỡng tồn kho hoặc ngưỡng chỗ trống ở Câu 2, rule xác định trạng thái nên đặt ở Controller, Service hay Repository? Giải thích lựa chọn của em.  
4\. Nếu một Service tự new Repository bên trong method, tại sao việc thay Repository thật bằng Fake Repository để kiểm tra logic sẽ khó khăn?  
5\. Nếu em thêm một property mới vào Entity nhưng quên tạo migration, hoặc đã tạo migration nhưng quên chạy database update, ứng dụng có thể gặp lỗi gì?  
6\. Nếu trang danh sách đọc 10.000 record nhưng không dùng AsNoTracking, hệ thống có thể tốn thêm tài nguyên như thế nào? Khi nào không nên dùng AsNoTracking?  
7\. Nếu nghiệp vụ tạo giao dịch đã lưu bản ghi chính nhưng bước trừ tồn kho hoặc giảm số chỗ bị lỗi, dữ liệu sẽ sai như thế nào nếu không có transaction?  
8\. Hãy mô tả cách em chủ động kiểm tra tình huống rollback. Em sẽ tạo dữ liệu đầu vào nào, kiểm tra những bảng nào và mong đợi kết quả gì?  
9\. Nếu config được đọc bằng string key ở nhiều nơi, khi đổi tên key hoặc đổi môi trường triển khai sẽ có rủi ro gì? Options Pattern giúp giảm rủi ro đó như thế nào?  
10\. Nếu cùng một request có Service và Repository đều cần DbContext, việc dùng Scoped lifetime giúp ích gì cho tính nhất quán của dữ liệu?  
11\. Seed Data có thể gây trùng dữ liệu trong trường hợp nào? Em sẽ thiết kế seed như thế nào để dễ kiểm soát và không tạo dữ liệu lặp ngoài ý muốn?  
12\. Nếu cần mở rộng transaction từ một item thành nhiều item, em cần kiểm tra những điều kiện nào trước khi commit để tránh tạo giao dịch nửa đúng nửa sai?  
13\. Hãy đề xuất cách chia Controller, Service và Repository cho chức năng lọc dữ liệu theo Relationship và khoảng giá trị đã làm ở Câu 2\.  
14\. Hãy mô tả một kịch bản kiểm tra ProductService hoặc Service tương đương bằng Fake Repository theo ba bước Arrange \- Act \- Assert.

## **Yêu cầu trả lời**

· Trình bày ngắn gọn nhưng phải có lập luận.  
· Gắn trực tiếp với ứng dụng mà em đã chọn ở Câu 1\.  
· Không trả lời chung chung hoặc chỉ chép lại định nghĩa.  
· Phải có ví dụ minh họa theo Entity, Service, Repository hoặc nghiệp vụ của bài làm.  
· Có thể dùng sơ đồ luồng hoặc ảnh chụp code để làm rõ câu trả lời.

## 

## **Yêu cầu nộp bài**

· File báo cáo: MSSV\_HoTenSinhVien\_ASP\_Lab04.pdf.  
· Báo cáo cần ghi rõ đã hoàn thành đầy đủ hay còn thiếu chức năng nào.  
· Đính kèm GitHub repository hoặc đường dẫn source code.  
· Chụp ảnh cấu trúc project, migration, database, giao diện chính, transaction thành công và trường hợp rollback.  
· Chụp ảnh hoặc mô tả kết quả của các feature ở Câu 2\.  
· Trình bày câu trả lời Problem Solving của Câu 3 trong cùng file báo cáo.  
· Commit message cần rõ nghĩa, không dùng các nội dung chung chung như update, fix hoặc final.

**Ghi chú:**  
Bài thực hành: ASP Lab04  
Tên file nộp: MSSV\_HoTenSinhVien\_ASP\_Lab04.pdf  
Nội dung báo cáo: ảnh chụp kết quả chạy, mô tả các feature đã làm và phần trả lời câu hỏi.  
Gửi mail : [tuantran261083course@gmail.com](mailto:tuantran261083course@gmail.com)   
Nội Dung Email :   
\- Ghi đã làm đầy đủ hay thiếu bài gì, chức năng gì   
\- Ghi có làm thêm được gì hay không (khuyến khích làm thêm để cộng điểm)  
\- Nếu nộp trễ ghi lý do nộp trễ   
\- Dealine : 15/06/2026 