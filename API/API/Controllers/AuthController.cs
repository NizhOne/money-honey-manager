using System;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAuthService authService;
        private readonly IEmailService emailService;
        private readonly AuthenticationOptions authConfig;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AuthenticationOptions> authOptions,
            IAuthService authService,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
            this.emailService = emailService;
            this.authConfig = authOptions.Value;
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await this.emailService.SendConfirmationMail(user, code);

                return this.authService.GenerateJwtToken(user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return Ok(this.authService.GenerateJwtToken(appUser));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Empty userId or code");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid userId ");
            }

            var result = await userManager.ConfirmEmailAsync(user, code.Replace(" ", "+"));
            if (result.Succeeded)
                return Ok(this.authService.GenerateJwtToken(user));
            else
                return BadRequest("Invalid userId or code");
        }

        [HttpPost()]
        public async Task<IActionResult> FacebookAuthentication([FromBody]FacebookAuthViewModel model)
        {
            var userInfo = await this.authService.GetFacebookUserInfoAsync(model.AccessToken);
            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    Name = userInfo.FirstName,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                };

                var result = await userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
            }

            var localUser = await userManager.FindByNameAsync(userInfo.Email);

            return new OkObjectResult(this.authService.GenerateJwtToken(localUser));
        }
    }
}