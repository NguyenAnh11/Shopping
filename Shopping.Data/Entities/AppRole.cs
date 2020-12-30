using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Shopping.Data.Entities
{
    public class AppRole:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
