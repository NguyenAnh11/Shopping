using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.System.User
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
