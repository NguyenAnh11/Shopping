using Shopping.ViewModel.Catalog.System.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shopping.Data.Entities;
using Shopping.Utilities.Exceptions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.ViewModel.Common;
using Microsoft.Extensions.Options;

namespace Shopping.Application.Catalog.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly JwtOptionConfiguration _jwtOption;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                    RoleManager<AppRole> roleManager,
                    IOptions<JwtOptionConfiguration> jwtOption)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtOption = jwtOption.Value;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.DateOfBirth,user.DoB.ToString()),
                new Claim(ClaimTypes.Role,String.Join(',',roles)),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
            };

            string key = _jwtOption.Key;
            string issuer = _jwtOption.Issuer;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:issuer,
                audience:issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            bool isExistUser = await _userManager.FindByEmailAsync(request.Email) == null ? false : true;
            if (isExistUser == true) return false;

            var user = new AppUser()
            {
                FristName = request.FristName,
                LastName = request.LastName,
                DoB = request.DoB,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return false;
            return true;
        }
    }
}
