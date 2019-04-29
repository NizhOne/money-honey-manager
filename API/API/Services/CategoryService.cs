using API.Constants;
using API.Models.Domain;
using API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MoneyHoneyDbContext dbContext;

        public CategoryService(MoneyHoneyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid AddCategory(CategoryDto category, ApplicationUser user)
        {
            var createdCategory = new Category
            {
                Name = category.Name,
                Type = category.Type,
                IsStandart = false,
                Creator = user
            };

            this.dbContext.Categories.Add(createdCategory);
            this.dbContext.SaveChanges();

            return createdCategory.Id;
        }

        public IEnumerable<CategoryDto> GetCategories(CategoryType type, ApplicationUser user)
        {
            return this.dbContext.Categories
              .Where(c => c.Type == type && (c.CreatorId == user.Id || c.IsStandart == true))
              .Select(c => new CategoryDto
              {
                  Id = c.Id,
                  CreatorId = c.CreatorId,
                  Name = c.Name,
                  IsStandart = c.IsStandart,
                  Type = c.Type
              }).ToList();
        }
    }
}
