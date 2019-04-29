using API.Interfaces;
using API.Models;
using API.Models.Domain;
using API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Services
{
    public class OperationService : IOperationService
    {
        private readonly MoneyHoneyDbContext dbContext;

        public OperationService(MoneyHoneyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<OperationDto> GetOperations(OperationsRequest request, ApplicationUser user)
        {
            Expression<Func<Operation, bool>> exp;

            exp = o => (o.CreatorId == user.Id)
                 && (request.CategoriesIds == null || request.CategoriesIds.Contains(o.Category.Id))
                 && (request.CategoryType == null || o.Category.Type == request.CategoryType)
                 && (request.From == null || o.Date > request.From)
                 && (request.To == null || o.Date < request.To)
                 && (request.LessThen == null || o.Amount < request.LessThen)
                 && (request.MoreThen == null || o.Amount > request.MoreThen);

            return this.dbContext.Operations
                .Include(o => o.Category)
                .Where(exp)
                .Select(o => new OperationDto
                {
                    Id = o.Id,
                    Category = new CategoryDto
                    {
                        Id = o.Category.Id,
                        CreatorId = o.Category.CreatorId,
                        Name = o.Category.Name,
                        IsStandart = o.Category.IsStandart,
                        Type = o.Category.Type
                    },
                    CategoryId = o.CategoryId,
                    Amount = o.Amount,
                    Date = o.Date,
                    Notes = o.Notes
                }).ToList();
        }

        public Guid AddOperation(OperationDto operation, ApplicationUser user)
        {
            var createdOperation = new Operation
            {
                Creator = user,
                CategoryId = operation.CategoryId,
                Amount = operation.Amount,
                Date = operation.Date,
                Notes = operation.Notes
            };

            this.dbContext.Operations.Add(createdOperation);
            this.dbContext.SaveChanges();

            return operation.Id;
        }
    }
}
