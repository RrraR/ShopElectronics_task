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
        public ActionResult Login([FromBody] LogInRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_userService.GetUser(request.UserName, request.Password).Result)
            {
                return Unauthorized();
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            
            return Ok(new LogInResultDto
            {
                Username = request.UserName,
                AccessToken = jwtResult.AccessToken,
            });
        }
        
        // [HttpGet("user")]
        // [Authorize]
        // public async ActionResult GetCurrentUser()
        // {
        //     // var temp = new LogInResultDto()
        //     // {
        //     //     UserName = User.Identity?.Name,
        //     //     AccessToken = _jwtAuthManager.GenerateTokens(User.Identity?.Name, new []{})
        //     // };
        //     return Ok(new LogInResultDto
        //     {
        //         UserName = User.Identity?.Name
        //     });
        // }

        [HttpPost("register")]
        [AllowAnonymous]
        public UserDto Register()
        {
            return null;
        }
    }
}
