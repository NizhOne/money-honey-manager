using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Constants;
using API.Models.Domain;
using API.Models.Dto;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoryService categoryService;

        public CategoryController(UserManager<ApplicationUser> userManager,
            ICategoryService categoryService)
        {
            this.userManager = userManager;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetAsync(CategoryType categoryType)
        {
            var appUser = await userManager.FindByNameAsync(User.Identity.Name);

            return categoryService.GetCategories(categoryType, appUser);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CategoryDto category)
        {
            var appUser = await userManager.FindByNameAsync(User.Identity.Name);

            Guid categoryId = this.categoryService.AddCategory(category, appUser);

            return Ok(categoryId);
        }
    }
}