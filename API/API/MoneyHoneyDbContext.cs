using API.Constants;
using API.Models;
using API.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace API
{
    public class MoneyHoneyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MoneyHoneyDbContext(DbContextOptions<MoneyHoneyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id=new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Food",
                IsStandart = true,
                Type = CategoryType.Waste
            },
            new Category
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "Transport",
                IsStandart = true,
                Type = CategoryType.Waste
            },
            new Category
            {
                Id = new Guid("00000000-0000-0000-0000-000000000003"),
                Name = "Life",
                IsStandart = true,
                Type = CategoryType.Waste
            },
            new Category
            {
                Id = new Guid("00000000-0000-0000-0000-000000000004"),
                Name = "Entertainment",
                IsStandart = true,
                Type = CategoryType.Waste
            },
            new Category
            {
                Id = new Guid("00000000-0000-0000-0000-000000000005"),
                Name = "Medicine",
                IsStandart = true,
                Type = CategoryType.Waste
            },
             new Category
             {
                 Id = new Guid("00000000-0000-0000-0000-000000000006"),
                 Name = "Salary",
                 IsStandart = true,
                 Type = CategoryType.Income
             }, 
             new Category
             {
                 Id = new Guid("00000000-0000-0000-0000-000000000007"),
                 Name = "Prostitution",
                 IsStandart = true,
                 Type = CategoryType.Income
             }
           );
        }
    }
}
