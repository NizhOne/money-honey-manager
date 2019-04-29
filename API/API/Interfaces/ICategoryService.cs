using System;
using System.Collections.Generic;
using API.Constants;
using API.Models.Domain;
using API.Models.Dto;

namespace API.Services
{
    public interface ICategoryService
    {
        Guid AddCategory(CategoryDto category, ApplicationUser user);
        IEnumerable<CategoryDto> GetCategories(CategoryType type, ApplicationUser user);
    }
}