using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Shopping.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
