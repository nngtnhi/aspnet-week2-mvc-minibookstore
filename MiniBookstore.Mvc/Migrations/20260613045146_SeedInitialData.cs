using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniBookstore.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Công nghệ thông tin" },
                    { 2, "Kỹ năng sống" },
                    { 3, "Khoa học" },
                    { 4, "Văn học" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "GenreId", "Isbn", "Price", "Publisher", "StockQuantity", "Title" },
                values: new object[,]
                {
                    { 1, 1, "ISBN-978-01", 150000m, "NXB Trẻ", 25, "C# Programming Fundamentals" },
                    { 2, 2, "ISBN-978-02", 95000m, "NXB Tổng Hợp", 5, "Đắc Nhân Tâm" },
                    { 3, 1, "ISBN-978-03", 450000m, "Prentice Hall", 3, "Clean Code" },
                    { 4, 3, "ISBN-978-04", 200000m, "NXB Tri Thức", 15, "Sapiens - Lược Sử Loài Người" },
                    { 5, 1, "ISBN-978-05", 520000m, "O'Reilly Media", 3, "Design Patterns" },
                    { 6, 4, "ISBN-978-06", 75000m, "NXB Văn Học", 30, "Nhà Giả Kim" },
                    { 7, 1, "ISBN-978-07", 680000m, "Manning Publications", 8, "ASP.NET Core in Action" },
                    { 8, 2, "ISBN-978-08", 85000m, "NXB Hội Nhà Văn", 2, "Tuổi Trẻ Đáng Giá Bao Nhiêu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
