using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shopping.ViewModel.Catalog.System.User;

namespace Shopping.Application.Catalog.System.User
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
