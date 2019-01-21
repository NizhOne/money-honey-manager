using API.Models;
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
    }
}
