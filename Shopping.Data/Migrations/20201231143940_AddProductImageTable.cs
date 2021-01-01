using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 30, 9, 31, 56, 970, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 500, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a47fc389-8774-4625-b373-df35334a2606"),
                column: "ConcurrencyStamp",
                value: "1b2eced7-177f-496a-aba0-57b57bef5049");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "173ba8fe-2774-4e44-ab17-88c003856593", "AQAAAAEAACcQAAAAEOUteMCM7MCp2yzvMlngF2KwYID20LbZNedP2Afy5y0gceZSKpvYXYsG/yf47bhkmQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 31, 21, 39, 33, 239, DateTimeKind.Local).AddTicks(406));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 30, 9, 31, 56, 970, DateTimeKind.Local).AddTicks(453),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a47fc389-8774-4625-b373-df35334a2606"),
                column: "ConcurrencyStamp",
                value: "7ab45a11-d1d3-41ed-8868-5f99af16ebcf");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2e6cfba-9d4e-4c76-aa35-55b4d679e524", "AQAAAAEAACcQAAAAEAal6x3lUNfGrfw/L22D5a1maSmBmHSUPQdaigVx107gExIJaDXPjroolUEuJBvT7w==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 30, 9, 31, 56, 996, DateTimeKind.Local).AddTicks(8680));
        }
    }
}
