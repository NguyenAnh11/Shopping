using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;


namespace Shopping.Data.EF
{
    public class ShoppingDBContextFactory : IDesignTimeDbContextFactory<ShoppingDBContext>
    {
        public ShoppingDBContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var connectionString = builder.GetValue<string>("ConnectionStrings:ShoppingDb");
            var optionBuilder = new DbContextOptionsBuilder<ShoppingDBContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new ShoppingDBContext(optionBuilder.Options);
        }
    }
}
