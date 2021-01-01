using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Data.Migrations
{
    public partial class AddProductImageDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a47fc389-8774-4625-b373-df35334a2606"),
                column: "ConcurrencyStamp",
                value: "c02ef2ca-9e05-4ac2-987d-4d06e5378041");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e0890e8d-9dcd-4f28-8e1f-f8878f391be3", "AQAAAAEAACcQAAAAED3ViZ2SEPBstRKWfUxZi677BMMhz5tmkUvrrdBo4Kh1NeK2p//d4Y6WLMPWKO3xDw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 1, 20, 58, 13, 320, DateTimeKind.Local).AddTicks(7124));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
