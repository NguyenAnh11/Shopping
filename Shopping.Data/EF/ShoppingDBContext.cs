using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Data.EF
{
    public class ShoppingDBContext : DbContext
    {
        public ShoppingDBContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}
