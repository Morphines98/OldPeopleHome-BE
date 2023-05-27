using MeerPflege.API.DTOs;
using MeerPflege.API.Services;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null || user.IsInactive)
                return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            var roles = await _userManager.GetRolesAsync(user);
            if(result.Succeeded)
                return new UserDto{
                    Token = _tokenService.CreateToken(user, roles.FirstOrDefault()),
                    Email = user.Email,
                    Role = roles.FirstOrDefault()
                };
            
            return Unauthorized();
        }

        [HttpPost("addUser")]
        public async Task<ActionResult<UserDto>> AddUser(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null)
            {
                var rez = await _userManager.CreateAsync(new AppUser(){UserName = login.Email, Email = login.Email, HomeId = 1}, login.Password);
                user = await _userManager.FindByEmailAsync(login.Email);
                await _userManager.AddToRoleAsync(user, Role.Admin);
                return Ok();
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            return Unauthorized();
        }
    }
}