using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAuthService authService;
        private readonly Authentication authConfig;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<Authentication> authOptions,
            IAuthService authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
            this.authConfig = authOptions.Value;
        }

        [HttpPost("[action]")]
        public async Task<object> Register(string name, string email, string password)
        {
            var user = new ApplicationUser
            {
                Name = name,
                UserName = email,
                Email = email
            };
            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return this.authService.GenerateJwtToken(email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost("[action]")]
        public async Task<string> Login(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == email);
                return this.authService.GenerateJwtToken(email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }
    }
}