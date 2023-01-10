using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ShopElectronics.Authentication;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtAuthManager _jwtAuthManager;

        public AccountController(IUserService userService, JwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] AuthRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_userService.GetUser(request.UserName, request.Password).Result)
            {
                return BadRequest();
            }
            
            var role = _userService.GetUserRole(request.UserName);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role, role.Result)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            
            return Ok(new AuthResultDto
            {
                Username = request.UserName,
                Role = role.Result,
                AccessToken = jwtResult.AccessToken,
            });
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] AuthRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if (_userService.IsAnExistingUser(request.UserName).Result)
            {
                //user already exists
                return BadRequest();
            }

            var user = await _userService.RegisterUser(request.UserName, request.Password);

            var role = _userService.GetUserRole(user.Username);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role, role.Result)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);
            
            return Ok(new AuthResultDto
            {
                Username = user.Username,
                Role = role.Result,
                AccessToken = jwtResult.AccessToken,
            });
        }
    }
}
