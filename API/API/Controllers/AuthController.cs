using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Models.Domain;
using API.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await this.emailService.SendConfirmationMail(user, code);

                    return Ok(this.authService.GenerateJwtToken(user));
                }
                else
                {
                    return BadRequest(this.GetIdentityErrors(result));
                }
            }
            else
            {
                return BadRequest(this.GetModelStateErrors(ModelState));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false);

            if (signInResult.Succeeded)
            {
                var appUser = await userManager.FindByNameAsync(model.Email);

                return Ok(this.authService.GenerateJwtToken(appUser));
            }
            else
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if(user == null)
                {
                    ValidationError validationError = new ValidationError();
                    validationError.Field = "Username";
                    validationError.Message = "Account with this username doesn't exist";
                    return BadRequest(validationError);
                }
                else
                {
                    ValidationError validationError = new ValidationError();
                    validationError.Field = "Password";
                    validationError.Message = "Incorrect password, please try again";
                    return BadRequest(validationError);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return BadRequest(new ValidationError
                    {
                        Field = "userId",
                        Message = "Invalid userId"
                    });
                }

                IdentityResult result = await userManager.ConfirmEmailAsync(user, code.Replace(" ", "+"));

                if (result.Succeeded)
                {
                    return Ok(this.authService.GenerateJwtToken(user));
                }
                else
                {
                    return BadRequest(new ValidationError
                    {
                        Message = "Invalid userId or confirmation code"
                    });
                }
            }
            else
            {
                return BadRequest(this.GetModelStateErrors(ModelState));
            }
        }

        [HttpPost()]
        public async Task<IActionResult> FacebookAuthentication([FromBody]FacebookAuthViewModel model)
        {
            FacebookUserData userInfo = await this.authService.GetFacebookUserInfoAsync(model.AccessToken);
            ApplicationUser user = await userManager.FindByNameAsync(userInfo.Email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Name = userInfo.FirstName,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                };

                var result = await userManager.CreateAsync(user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
            }

            return Ok(this.authService.GenerateJwtToken(user));
        }

        private IEnumerable<ValidationError> GetModelStateErrors(ModelStateDictionary modelState)
        {
            return modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(error =>
                    new ValidationError
                    {
                        Field = key,
                        Message = error.ErrorMessage
                    }))
                .ToList();
        }

        private IEnumerable<ValidationError> GetIdentityErrors(IdentityResult result)
        {
            return result.Errors.Select(error =>
                new ValidationError
                {
                    Message = error.Description
                })
                .ToList();
        }
    }
}