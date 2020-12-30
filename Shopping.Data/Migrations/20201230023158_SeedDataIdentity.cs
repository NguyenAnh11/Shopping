using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Data.Migrations
{
    public partial class SeedDataIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 30, 9, 31, 56, 970, DateTimeKind.Local).AddTicks(453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 30, 9, 15, 30, 195, DateTimeKind.Local).AddTicks(2066));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("a47fc389-8774-4625-b373-df35334a2606"), "7ab45a11-d1d3-41ed-8868-5f99af16ebcf", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a47fc389-8774-4625-b373-df35334a2606"), new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FristName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"), 0, "e2e6cfba-9d4e-4c76-aa35-55b4d679e524", new DateTime(2001, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhnguyenviet11299@gmail.com", true, "Viet", "Anh", false, null, null, null, "AQAAAAEAACcQAAAAEAal6x3lUNfGrfw/L22D5a1maSmBmHSUPQdaigVx107gExIJaDXPjroolUEuJBvT7w==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 30, 9, 31, 56, 996, DateTimeKind.Local).AddTicks(8680));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a47fc389-8774-4625-b373-df35334a2606"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a47fc389-8774-4625-b373-df35334a2606"), new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1a529cf-41ce-46a1-a155-453dd775cca2"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 30, 9, 15, 30, 195, DateTimeKind.Local).AddTicks(2066),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 30, 9, 31, 56, 970, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 30, 9, 15, 30, 209, DateTimeKind.Local).AddTicks(2965));
        }
    }
}
