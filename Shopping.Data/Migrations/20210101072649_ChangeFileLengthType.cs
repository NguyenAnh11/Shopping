using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a47fc389-8774-4625-b373-df35334a2606"),
                column: "ConcurrencyStamp",
                value: "7a01b1b0-222c-4de2-aaf2-27e39d710056");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6f37b76-4f53-4ca8-8c27-e5cde7fb0329", "AQAAAAEAACcQAAAAEI6IT+X0pKNx2B2PENAtnETqDACDuoh0kEgF+f/YoBev4t+1bh0a4lLn6ujvJPS9lw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 1, 14, 26, 47, 532, DateTimeKind.Local).AddTicks(8803));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
        }
    }
}
