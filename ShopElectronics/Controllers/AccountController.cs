using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        
        
        
        // [HttpGet("user")]
        // [Authorize]
        // public async ActionResult GetCurrentUser()
        // {
        //     // var temp = new AuthResultDto()
        //     // {
        //     //     UserName = User.Identity?.Name,
        //     //     AccessToken = _jwtAuthManager.GenerateTokens(User.Identity?.Name, new []{})
        //     // };
        //     return Ok(new AuthResultDto
        //     {
        //         UserName = User.Identity?.Name
        //     });
        // }
    }
}
