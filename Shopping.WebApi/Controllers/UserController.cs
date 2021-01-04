using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Shopping.Data.Entities;
using Shopping.Application.Catalog.System.User;
using Shopping.ViewModel.Catalog.System.User;
using Microsoft.AspNetCore.Authorization;
using Shopping.ViewModel.Common;

namespace Shopping.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(new { Token = token });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (result == false) return BadRequest("Fail");

            return Ok("Success");
        }
    }
}
