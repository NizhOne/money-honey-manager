using API.Models;
using API.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}
